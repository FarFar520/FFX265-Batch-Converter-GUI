using System;

namespace FFX265_Batch_Converter {
    internal class FFmpegParams {

        public bool map0a = true, map0s = false, oneThread = true;
        public float crf = 22.0f;
        public float gop_sec = 5.0f;
        public int gop = 1;
        public int iPreset = 0;
        public bool vfr = false;
        public string scaleto = "100%";

        public bool b_gop_sec = false;

        public bool acopy = false;
        public string ac = string.Empty, channel = string.Empty;
        public string acodec = "copy";

        public bool b_add_lavfi = false;
        string _str_add_lavfi = string.Empty;
        public string str_add_lavfi {
            get { return _str_add_lavfi; }
            set { _str_add_lavfi = value.Trim( ).Trim(','); }
        }
        public bool b_add_lavfi_set {
            get { return b_add_lavfi && _str_add_lavfi.Length > 1; }
        }

        public static float f看不出畸变 = (1080 - 16) / 1080f;


        //public float ss_input = 0, ss_output = 0, to = 0;//每个视频不同

        public bool set_Scale => Setting.b校正为DAR比例 || scaleto != "100%";//压制老片存在用DAR比例来修正编码比例。加个前置判断。

        public void shrink(float display_Width, ref float outWidth, ref float outHeight, out bool flag, out string str) {
            flag = false;
            str = string.Empty;
            if (scaleto == "↓50%") {
                str = "scale=iw*0.5:ih*0.5:flags=bicubic";
                outWidth = display_Width / 2; outHeight /= 2;
            } else if (scaleto == "↓25%") {
                str = "scale=iw*0.25:ih*0.25:flags=bicubic";
                outWidth = display_Width / 4; outHeight /= 4;
            } else if (scaleto == "↓1280×720p") {
                flag = true;
                if (display_Width > 1280 - 16) {
                    str = "scale=1280:-4";
                    outHeight = 1280 / display_Width * outHeight;
                    outWidth = 1280;
                } else if (outHeight > 1280 - 16) {
                    str = "scale=-4:1280";
                    outWidth = 1280 / outHeight * display_Width;
                    outHeight = 1280;
                }
            } else if (scaleto == "↓1920×1080p") {
                flag = true;
                if (display_Width > 1920 - 32) {
                    str = "scale=1920:-4";
                    outHeight = 1920 / display_Width * outHeight;
                    outWidth = 1920;
                } else if (outHeight > 1920 - 32) {
                    outWidth = 1920 / outHeight * display_Width;
                    outHeight = 1920;
                    str = "scale=-4:1920";
                }
            } else if (scaleto == "↓2560×1440p") {
                flag = true;
                if (display_Width > 2560 - 48) {
                    str = "scale=2560:-4";
                    outHeight = 2560 / display_Width * outHeight;
                    outWidth = 2560;
                } else if (outHeight > 2560 - 48) {
                    str = "scale=-4:2560";
                    outWidth = 2560 / outHeight * display_Width;
                    outHeight = 2560;
                }
            } else if (scaleto == "↓3840×2160p") {
                flag = true;
                if (display_Width > 3840 - 64) {
                    str = "scale=3840:-4";
                    outHeight = 3840 / display_Width * outHeight;
                    outWidth = 3840;
                } else if (outHeight > 3840 - 64) {
                    str = "scale=-4:3840";
                    outWidth = 3840 / outHeight * outHeight;
                    outHeight = 3840;
                }
            } else if (scaleto.StartsWith("↓长边")) {
                if (int.TryParse(Setting.regexD.Match(scaleto).Value, out int max)) {
                    if (max >= X265Params.MIN_PIX) {
                        if (display_Width >= outHeight) {//正方形视频当宽大于高处理
                            str = $"scale={max}:-4";
                        } else {
                            str = $"scale=-4:{max}";
                        }
                    }
                }
            }

            //Some codecs require the size of width and height to be a multiple of n. You can achieve this by setting the width or height to -n:
        }

        public bool scale(int inWidth, int inHeight, int darW, int darH, ref float outWidth, ref float outHeight, out string str) {
            str = string.Empty;
            bool flag = false;

            float original_display_Width = inWidth;

            if (darW > 0 && darH > 0) original_display_Width = inHeight * darW / darH;//假定长边是宽（1440×1080），实际可能包含竖屏1080 ×1440

            bool autoZoom = original_display_Width != inWidth;//&& ((darW == 16 && darH == 9) || (darW == 9 && darH == 16));
            //自动校正为Original display aspect ratio比例，如 1440×1080i DAR 16：9  → 1920×1080p DAR 16：9

            if (autoZoom) {//变比例缩放
                float out_display_Width = original_display_Width * (outWidth / inWidth);//用双精浮点型变量方便计算比例，最后再取整。
                float out_Display_Height = outHeight;

                float undetectable = 1887.0f / 1920;//相机硬件编码、软件渲染引起4~32像素黑边，轻度剪裁后，画面可以轻微放大回原始尺寸，无法察觉。

                if (inWidth > out_display_Width) {
                    if (out_display_Width / inWidth > undetectable) out_display_Width = original_display_Width = inWidth;//畸变较小时，不缩放。

                } else {
                    if (inWidth / out_display_Width > undetectable) out_display_Width = original_display_Width = inWidth;//畸变较小时，不缩放。
                }

                if (outHeight < inHeight && outHeight / inHeight > undetectable) {//高度有几个像素轻度剪裁，判断符合不可查觉则放大回原始尺寸。
                    out_Display_Height = inHeight;
                }
                if (outWidth < inWidth && outWidth / inWidth > undetectable) {//宽度有几个像素轻度剪裁，判断符合不可查觉则放大回原始尺寸。
                    out_display_Width = original_display_Width;
                }
                flag = true;
                if (scaleto == "100%") {
                    if (Setting.b校正为DAR比例) {
                        str = $"scale={out_display_Width}:{out_Display_Height}";
                        outWidth = out_display_Width;
                        outHeight = out_Display_Height;
                    } else return false;//视频比例畸变，发生轻微剪裁时，不处理轻微拉伸。
                } else {
                    if (Setting.b校正为DAR比例) {
                        if (scaleto == "↓50%") {
                            out_display_Width /= 2; out_Display_Height /= 2;
                        } else if (scaleto == "↓25%") {
                            out_display_Width /= 4; out_Display_Height /= 4;
                        } else if (scaleto == "↓1280×720p") {
                            if (out_display_Width > 1280 - 16) {//横屏
                                out_Display_Height = out_Display_Height * (1280 / out_display_Width);//以长边为准计算输出视频高度。
                                out_display_Width = 1280;

                            } else if (out_Display_Height > 1280 - 16) {
                                out_display_Width = out_display_Width * (1280 / out_Display_Height);//以长边为准计算输出视频高度。
                                out_Display_Height = 1280;
                            }
                        } else if (scaleto == "↓1920×1080p") {
                            if (out_display_Width > 1920 - 32) {//横屏
                                out_Display_Height = out_Display_Height * (1920 / out_display_Width);//以长边为准计算输出视频高度。
                                out_display_Width = 1920;
                            } else if (out_Display_Height > 1920 - 32) {
                                out_display_Width = out_display_Width * (1920 / out_Display_Height);//以长边为准计算输出视频高度。
                                out_Display_Height = 1920;
                            }
                        } else if (scaleto == "↓2560×1440p") {
                            if (out_display_Width > 2560 - 48) {//横屏
                                out_Display_Height = out_Display_Height * (2560 / out_display_Width);//以长边为准计算输出视频高度。
                                out_display_Width = 2560;
                            } else if (out_Display_Height > 2560 - 48) {
                                out_display_Width = out_display_Width * (2560 / out_Display_Height);//以长边为准计算输出视频高度。
                                out_Display_Height = 2560;
                            }
                        } else if (scaleto == "↓3840×2160p") {
                            if (out_display_Width > 3840 - 64) {//横屏
                                out_Display_Height = out_Display_Height * (3840 / out_display_Width);//以长边为准计算输出视频高度。
                                out_display_Width = 3840;
                            } else if (out_Display_Height > 3840 - 64) {
                                out_display_Width = out_display_Width * (3840 / out_Display_Height);//以长边为准计算输出视频高度。
                                out_Display_Height = 3840;
                            }
                        } else if (scaleto.StartsWith("↓长边")) {
                            if (int.TryParse(Setting.regexD.Match(scaleto).Value, out int max)) {
                                if (max >= X265Params.MIN_PIX) {
                                    if (out_display_Width >= out_Display_Height) {//正方形视频当宽大于高处理
                                        if (max > out_display_Width) {
                                            out_display_Width = max;
                                            out_Display_Height *= (max / out_display_Width);
                                        }
                                    } else {
                                        if (max > out_Display_Height) {
                                            out_Display_Height = max;
                                            out_display_Width *= (max / out_Display_Height);
                                        }
                                    }
                                }
                            }
                        }

                        out_display_Width = (float)Math.Round(out_display_Width);
                        out_Display_Height = (float)Math.Round(out_Display_Height);

                        int modW = (int)out_display_Width % 4;
                        if (modW > 0) out_display_Width = out_display_Width - modW + 4;

                        int modH = (int)out_Display_Height % 4;//x265常见最小块=4×4；
                        if (modH > 0) out_Display_Height = out_Display_Height - modH + 4;

                        outWidth = out_display_Width;
                        outHeight = out_Display_Height;

                        str = $"scale={out_display_Width}:{out_Display_Height}";

                    } else {
                        shrink(out_display_Width, ref outWidth, ref outHeight, out flag, out str);
                    }
                }
            } else {
                if (scaleto == "100%") {
                    if (outHeight == inHeight && outWidth == inWidth) return false;
                    if (outHeight / inHeight >= f看不出畸变 && outWidth / inWidth >= f看不出畸变) {//剪裁量较小时（编码器引起的微量黑边），缩放回原始比例
                        flag = true;
                        str = $"scale={inWidth}:{inHeight}";
                        outWidth = inWidth; outHeight = inHeight;
                    } else if (inWidth > outWidth && inWidth - outWidth <= 64) {//左右发生少量剪裁，强行缩放回原始宽度
                        flag = true;
                        str = $"scale={inWidth}:-4";
                        outWidth = inWidth;
                    }
                } else {
                    shrink(outWidth, ref outWidth, ref outHeight, out flag, out str);
                }
            }

            if (flag && !string.IsNullOrEmpty(str))
                str += ":flags=bicubic";

            return !string.IsNullOrEmpty(str);
        }

        public string x26n_preset {
            get {
                if (iPreset == 0) {
                    return string.Empty;
                } else {
                    string param = " -preset ";
                    if (iPreset == 1) {
                        return param + "slow";
                    } else if (iPreset == 2) {
                        return param + "slower";
                    } else if (iPreset == 3) {
                        return param + "veryslow";
                    } else if (iPreset == 4) {
                        return param + "placebo";
                    } else if (iPreset == 6) {
                        return param + "ultrafast";
                    } else if (iPreset == 7) {
                        return param + "superfast";
                    } else if (iPreset == 8) {
                        return param + "veryfast";
                    } else if (iPreset == 9) {
                        return param + "faster";
                    } else if (iPreset == 10) {
                        return param + "fast";
                    } else
                        return string.Empty;
                }
            }
        }

        public void audio_Set(string set) {
            acopy = false;
            if (set == "转AAC立体声") {
                acodec = "aac";
                ac = " -ac 2";
                channel = "stereo";

            } else if (set == "转AAC单声道") {
                acodec = "aac";
                ac = " -ac 1";
                channel = "mono";
            } else {
                acopy = true;
                acodec = "copy";
                ac = string.Empty;
                channel = string.Empty;
            }
        }
    }
}
