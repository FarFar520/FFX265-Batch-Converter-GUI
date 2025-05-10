using System;

namespace FFX265_Batch_Converter {
    internal class JsonVideoStreams {


        public VStream[] streams { get; set; }
        public Format format { get; set; }


        public class Format {
            public string filename { get; set; }
            public int nb_streams { get; set; }
            public int nb_programs { get; set; }
            public int nb_stream_groups { get; set; }
            public string format_name { get; set; }
            public string format_long_name { get; set; }
            public string start_time { get; set; }
            public string duration { get; set; }
            public string size { get; set; }
            public string bit_rate { get; set; }
            public int probe_score { get; set; }
            public TagsFormat tags { get; set; }
        }

        public class TagsFormat {
            public string encoder { get; set; }
            public DateTime creation_time { get; set; }
        }

        public class VStream {
            public int index { get; set; }
            public string codec_name { get; set; }
            public string codec_long_name { get; set; }
            public string profile { get; set; }
            public string codec_type { get; set; }
            public string codec_tag_string { get; set; }
            public string codec_tag { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int coded_width { get; set; }
            public int coded_height { get; set; }
            public int closed_captions { get; set; }
            public int film_grain { get; set; }
            public int has_b_frames { get; set; }
            public string sample_aspect_ratio { get; set; }
            public string display_aspect_ratio { get; set; }
            public string pix_fmt { get; set; }
            public int level { get; set; }
            public string color_range { get; set; }
            public string color_space { get; set; }
            public string color_transfer { get; set; }
            public string color_primaries { get; set; }
            public string chroma_location { get; set; }
            public string field_order { get; set; }
            public int refs { get; set; }
            public string is_avc { get; set; }
            public string nal_length_size { get; set; }
            public string r_frame_rate { get; set; }
            public string avg_frame_rate { get; set; }
            public string time_base { get; set; }
            public int start_pts { get; set; }
            public string start_time { get; set; }
            public string bits_per_raw_sample { get; set; }
            public int extradata_size { get; set; }
            public Disposition disposition { get; set; }
            public TagsSub tags { get; set; }
            public string sample_fmt { get; set; }
            public string sample_rate { get; set; }
            public int channels { get; set; }
            public string channel_layout { get; set; }
            public int bits_per_sample { get; set; }
            public int initial_padding { get; set; }
            public int duration_ts { get; set; }
            public string duration { get; set; }
        }

        public class Disposition {
            public int _default { get; set; }
            public int dub { get; set; }
            public int original { get; set; }
            public int comment { get; set; }
            public int lyrics { get; set; }
            public int karaoke { get; set; }
            public int forced { get; set; }
            public int hearing_impaired { get; set; }
            public int visual_impaired { get; set; }
            public int clean_effects { get; set; }
            public int attached_pic { get; set; }
            public int timed_thumbnails { get; set; }
            public int non_diegetic { get; set; }
            public int captions { get; set; }
            public int descriptions { get; set; }
            public int metadata { get; set; }
            public int dependent { get; set; }
            public int still_image { get; set; }
        }

        public class TagsSub {
            public string language { get; set; }
            public string title { get; set; }
            public string BPS { get; set; }
            public string DURATION { get; set; }
            public string NUMBER_OF_FRAMES { get; set; }
            public string NUMBER_OF_BYTES { get; set; }
            public string _STATISTICS_WRITING_APP { get; set; }
            public string _STATISTICS_WRITING_DATE_UTC { get; set; }
            public string _STATISTICS_TAGS { get; set; }
        }

    }
}
