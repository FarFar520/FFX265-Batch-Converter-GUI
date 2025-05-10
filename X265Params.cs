using System;
using System.Text;

namespace FFX265_Batch_Converter {
    internal class X265Params {

        public const ushort MIN_PIX = 64;



        public int def_bframes = 4, def_keyint = 250, def_qpmin = 0, def_qpmax = 69;


        public int round = 8;

        string _preset = string.Empty;

        public string preset => _preset;

        public void setPreset(int _iPreset) {
            if (_iPreset == 0)
                _preset = "medium";
            def_bframes = 4;
            if (_iPreset == 1) {
                _preset = "slow";
                def_bframes = 4;
            } else if (_iPreset == 2) {
                _preset = "slower";
                def_bframes = 5;
            } else if (_iPreset == 3) {
                _preset = "veryslow";
                def_bframes = 5;
            } else if (_iPreset == 4) {
                _preset = "placebo";
                def_bframes = 5;
            } else if (_iPreset == 6) {
                _preset = "ultrafast";
                def_bframes = 3;
            } else if (_iPreset == 7) {
                _preset = "superfast";
                def_bframes = 3;
            } else if (_iPreset == 8) {
                _preset = "veryfast";
                def_bframes = 4;
            } else if (_iPreset == 9) {
                _preset = "faster";
                def_bframes = 4;
            } else if (_iPreset == 10) {
                _preset = "fast";
                def_bframes = 4;

            } else {
                _preset = "medium";
            }
        }

        public bool oneThread = true,
            keyintMax = false,keyintSet=false, fades = true, hist_scenecut,
            is_aq_mode = false, mcstf = false, is_nr_intra = false, is_nr_inter = false,
            frame_dup = false, hrd = false, vbv = false,
            analyze_src_pics = false, umh = false, rect = false, amp = false,
            rc_lookahead_halfkeyint = false,
            single_sei = false, no_info = false,
            bframes_thirdfps = false,
            qp_min_max = false
            ;

        
        int _aq_mode = 4;
        int _nr_intra = 0, _nr_inter = 0, _bframes = 0;

        float _qp_min = 0, _qp_max = 69;

        public int bframes {
            get { return _bframes; }
            set {
                if (value < 0)
                    _bframes = 0;
                else if (value > 16)
                    _bframes = 16;
                else _bframes = value;
            }
        }
        public int nr_intra {
            set {
                if (value < 0) _nr_intra = 0;
                else if (value > 2000) _nr_intra = 2000;
                else _nr_intra = value;
            }
        }
        public int nr_inter {
            set {
                if (value < 0) _nr_inter = 0;
                else if (value > 2000) _nr_inter = 2000;
                else _nr_inter = value;
            }
        }
        public int aq_mode {
            set {
                if (value > 1 && value < 5)
                    _aq_mode = value;
            }
        }

        public void qp_range(float min, float max) {
            if (min < 0) min = 0;
            else if (min > 69) min = 69;

            if (max < 0) max = 0;
            else if (max > 69) max = 69;

            if (min > max) {
                _qp_min = max; _qp_max = min;
            } else {
                _qp_min = min; _qp_max = max;
            }
        }

        void third_fps_bframes(double fps, ref StringBuilder stringBuilder) {
            int third = (int)(Math.Round(fps / 3));
            if (third > 16) third = 16;
            else if (mcstf & third < 5) third = 5;

            if (bframes_thirdfps) {
                stringBuilder.Append(":weightb=1");
                if (third > def_bframes) {
                    stringBuilder.Append(":bframes=").Append(third);
                }
            } else if (mcstf) {
                stringBuilder.Append(":bframes=5");
            }
        }

        void qp_range(float crf, ref StringBuilder stringBuilder) {
            if (qp_min_max) {
                if (_qp_min > 0) {
                    stringBuilder.Append(":qpmin=");
                    if (_qp_min <= crf)
                        stringBuilder.Append(_qp_min);
                    else
                        stringBuilder.Append(crf);
                }

                if (_qp_max < 69) {
                    stringBuilder.Append(":qpmax=");
                    if (_qp_max > -crf)
                        stringBuilder.Append(_qp_max);
                    else
                        stringBuilder.Append(crf);
                }
            }
        }


        public string getParams(int keyint, float crf, double tbr_out) {

            if (crf == 0) {
                if (oneThread)
                    return " -x265-params lossless=1:cu-lossless=1:pools=none:frame-threads=1:lookahead-threads=1:lookahead-slices=0:no-wpp=1";
                else
                    return " -x265-params lossless=1:cu-lossless=1";
            }

            StringBuilder stringBuilder = new StringBuilder( );

            if (oneThread) stringBuilder.Append(":pools=none:frame-threads=1:lookahead-threads=1:lookahead-slices=0:no-wpp=1");

            if (is_nr_intra && _nr_intra > 0) stringBuilder.AppendFormat(":nr-intra={0}", _nr_intra);
            if (is_nr_inter && _nr_inter > 0) stringBuilder.AppendFormat(":nr-inter={0}", _nr_inter);
            //完整版x265教程 2024.59.6 by Avoe建议1080p nr256以内，默认给64 。1920*1080/64=32400,画面等于噪点的3.24万倍。

            if (mcstf) stringBuilder.Append(":mcstf=1");

            third_fps_bframes(tbr_out, ref stringBuilder);//连续B帧数按视频帧的三分之一量来

            if (rect) stringBuilder.Append(":rect=1");
            if (amp) stringBuilder.Append(":amp=1");

            //if (frame_dup) stringBuilder.Append(":frame-dup=1");//流媒体相关，自动丢弃帧，暂时没使用选项

            if (!keyintSet && keyintMax)
                stringBuilder.Append(":keyint=-1:rc-lookahead=250");//keyint，外部参数 -g -1 不会传入， -g 0无画面。
            else if (rc_lookahead_halfkeyint && keyint > 40) {
                int lookahead = keyint / 2;
                stringBuilder.AppendFormat(":rc-lookahead={0}", lookahead > 250 ? 250 : lookahead);//完整版x265教程 2024.59.6 by Avoe 建议的搜索范围扩大为关键帧间隔的一半
            }

            if (is_aq_mode) stringBuilder.AppendFormat(":aq-mode={0}", _aq_mode);

            if (hist_scenecut) stringBuilder.Append(":hist-scenecut=1");
            if (fades) stringBuilder.Append(":fades=1");

            if (analyze_src_pics) stringBuilder.Append(":analyze-src-pics=1");

            if (umh) stringBuilder.Append(":me=umh");

            if (no_info) stringBuilder.Append(":no-info=1");
            else if (single_sei) stringBuilder.Append(":single-sei=1");

            //stringBuilder.Append(":crf-min=14:crf-max=28");
            //stringBuilder.Append(":qpmin=14:qpmax=28");  //qpmax=28暗场景画质能提升一些，体积增加7%

            qp_range(crf, ref stringBuilder);

            if (stringBuilder.Length > 1) {
                stringBuilder.Remove(0, 1);
                stringBuilder.Insert(0, " -x265-params ");
                return stringBuilder.ToString( );
            } else
                return string.Empty;

        }

    }
}
