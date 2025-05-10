using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FFX265_Batch_Converter {
    internal class FileUtil {

        [StructLayout(LayoutKind.Sequential)]
        struct RM_UNIQUE_PROCESS {
            public int dwProcessId;
            public System.Runtime.InteropServices.ComTypes.FILETIME ProcessStartTime;
        }

        const int RmRebootReasonNone = 0;
        const int CCH_RM_MAX_APP_NAME = 255;
        const int CCH_RM_MAX_SVC_NAME = 63;

        enum RM_APP_TYPE {
            RmUnknownApp = 0,
            RmMainWindow = 1,
            RmOtherWindow = 2,
            RmService = 3,
            RmExplorer = 4,
            RmConsole = 5,
            RmCritical = 1000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct RM_PROCESS_INFO {
            public RM_UNIQUE_PROCESS Process;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_APP_NAME + 1)]
            public string strAppName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_SVC_NAME + 1)]
            public string strServiceShortName;
            public RM_APP_TYPE ApplicationType;
            public uint AppStatus;
            public uint TSSessionId;
            [MarshalAs(UnmanagedType.Bool)]
            public bool bRestartable;
        }

        [DllImport("rstrtmgr.dll", CharSet = CharSet.Unicode)]
        static extern int RmStartSession(out uint pSessionHandle, int dwSessionFlags, string strSessionKey);

        [DllImport("rstrtmgr.dll")]
        static extern int RmEndSession(uint pSessionHandle);

        [DllImport("rstrtmgr.dll", CharSet = CharSet.Unicode)]
        static extern int RmRegisterResources(uint pSessionHandle, uint nFiles, string[] rgsFilenames,
                                            uint nApplications, [In] RM_UNIQUE_PROCESS[] rgApplications,
                                            uint nServices, string[] rgsServiceNames);

        [DllImport("rstrtmgr.dll")]
        static extern int RmGetList(uint dwSessionHandle, out uint pnProcInfoNeeded,
                                    ref uint pnProcInfo, [In, Out] RM_PROCESS_INFO[] rgAffectedApps,
                                    ref uint lpdwRebootReasons);

        /// <summary>
        /// 查找占用指定文件或文件夹的所有进程
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>占用该文件或文件夹的进程列表</returns>
        public static bool GetProcessesLockingFile(string filePath, out List<Process> processes) {
            processes = new List<Process>( );
            uint handle;
            string key = Guid.NewGuid( ).ToString( );
            int result = RmStartSession(out handle, 0, key);

            if (result != 0)
                return false;

            try {
                const int ERROR_MORE_DATA = 234;
                uint pnProcInfoNeeded = 0;
                uint pnProcInfo = 0;
                uint lpdwRebootReasons = RmRebootReasonNone;

                string[] resources = new string[] { filePath };
                result = RmRegisterResources(handle, (uint)resources.Length, resources, 0, null, 0, null);

                if (result != 0)
                    throw new Exception("无法注册资源");

                // 获取需要的进程信息数量
                result = RmGetList(handle, out pnProcInfoNeeded, ref pnProcInfo, null, ref lpdwRebootReasons);

                if (result == ERROR_MORE_DATA) {
                    // 分配足够的内存并获取进程信息
                    pnProcInfo = pnProcInfoNeeded;
                    RM_PROCESS_INFO[] processInfo = new RM_PROCESS_INFO[pnProcInfo];
                    result = RmGetList(handle, out pnProcInfoNeeded, ref pnProcInfo, processInfo, ref lpdwRebootReasons);
                    if (result == 0) {
                        for (int i = 0; i < pnProcInfo; i++) {
                            try {
                                processes.Add(Process.GetProcessById(processInfo[i].Process.dwProcessId));
                            } catch {
                                // 进程可能已经结束，忽略
                            }
                        }
                    } else {
                        return false;
                    }
                } else if (result != 0) {
                    return false;
                }
            } finally {
                RmEndSession(handle);
            }
            return processes.Count > 0;
        }

    }


}
