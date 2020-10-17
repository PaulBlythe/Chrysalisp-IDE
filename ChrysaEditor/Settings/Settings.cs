using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChrysaEditor.Settings
{
    public static class Settings
    {
        public static String HostPath = "";// @"C:\GitHub\ChrysaLisp";
        public static bool Dirty = false;

        public static String LispKeywords =
         "% * + - / /= < << <= = > >= >> >>> abi abs age align apply array ascii-lower ascii-upper base-functions base-macros bind cap cat catch char char-to-num clear cmp code cond copy cpu def defq each! each-line each-mergeable each-mergeable-rev elem elem-set empty? ends-with env eql erase eval even? exec f2i f2r ffi file-stream filter find find-rev first fixeds gensym get get-cstr hash i2f i2r import insert intern intern-seq io-stream join lambda last length list load load-stream log2 logand logior lognot logxor macro macroexpand map map-rev match? max merge-obj min neg neg? nempty? nil? nlo nlz nto ntz num-to-char num-to-utf8 nums odd? pad partition penv pop pos? pow prebind prin print progn push quasi-quote quote r2f r2i random range read read-avail read-char read-line reals reduce reduce-rev reduced-reduce reduced-reduce-rev repl rest reverse save second set setq shuffle shuffled slice some! sort sorted split starts-with str str-to-num string-stream swap sym throw time to-lower to-upper tolist trim trim-end trim-start type-of type-to-size undef unzip while within-compile-env write write-char write-line zip";

        public static String LispFunctions =
        "defun mail-send ui-label defun-bind def-func-end defmacro defcvar import defq defmacro-bind defmacro import cond structure ui-window window byte " +
        "def-struct def-struct-end def-func ffi each while not and or reduce setq when ui-title-bar bind gui-add view-hide view-event view-change-dirty ui-grid";

        public static String LispMacros =
        "# and ascii-char ascii-code case compose const curry dec defmacro defmacro-bind defun defun-bind each each-rev env? every fnc? get-byte get-int get-long get-short get-ubyte get-uint get-ushort if inc let lst? not notany notevery num? opt or rcurry read-int read-long read-short reduced seq? setd some str? sym? times unless until when write-int write-long write-short";

        public static String Symbols =
            " argb_black " +
        "argb_blue " +
        "argb_cyan " +
        "argb_green " +
        "argb_grey1 " +
        "argb_grey10 " +
        "argb_grey11 " +
        "argb_grey12 " +
        "argb_grey13 " +
        "argb_grey14 " +
        "argb_grey15 " +
        "argb_grey2 " +
        "argb_grey3 " +
        "argb_grey4 " +
        "argb_grey5 " +
        "argb_grey6 " +
        "argb_grey7 " +
        "argb_grey8 " +
        "argb_grey9 " +
        "argb_magenta " +
        "argb_red " +
        "argb_white " +
        "argb_yellow " +
        "byte_size " +
        "canvas_color " +
        "canvas_flags " +
        "cap_arrow " +
        "cap_butt " +
        "cap_round " +
        "cap_square " +
        "cap_tri " +
        "component_id " +
        "ev_msg_action_source_id " +
        "ev_msg_key_key " +
        "ev_msg_key_keycode " +
        "ev_msg_mouse_buttons " +
        "ev_msg_mouse_rx " +
        "ev_msg_mouse_ry " +
        "ev_msg_target_id " +
        "ev_msg_type " +
        "ev_type_gui " +
        "ev_type_key " +
        "ev_type_mouse " +
        "flow_flag_align_hcenter " +
        "flow_flag_align_hleft " +
        "flow_flag_align_hright " +
        "flow_flag_align_vbottom " +
        "flow_flag_align_vcenter " +
        "flow_flag_align_vtop " +
        "flow_flag_down " +
        "flow_flag_fillh " +
        "flow_flag_fillw " +
        "flow_flag_lasth " +
        "flow_flag_lastw " +
        "flow_flag_left " +
        "flow_flag_right " +
        "flow_flag_up " +
        "in_mbox_id " +
        "in_state " +
        "int_size " +
        "join_bevel " +
        "join_miter " +
        "join_round " +
        "kn_call_child " +
        "kn_call_open " +
        "load_flag_film " +
        "load_flag_noswap " +
        "load_flag_shared " +
        "long_size " +
        "out_state " +
        "ptr_size " +
        "scroll_flag_horizontal " +
        "scroll_flag_vertical " +
        "short_size " +
        "stdio_args " +
        "stream_mail_state_started " +
        "stream_mail_state_stopped " +
        "stream_mail_state_stopping " +
        "vdu_char_height " +
        "vdu_char_width " +
        "view_flag_at_back " +
        "view_flag_dirty_all " +
        "view_flag_opaque " +
        "view_flag_solid " +
        "view_flags " +
        "view_h " +
        "view_w " +
        "view_x " +
        "view_y " +
        "file_open_read " +
        "file_open_write " +
        "file_open_append";

		public static void Save()
        {
            if (Dirty)
            {
                String location = Path.Combine(Application.StartupPath, "settings.bin");
                FileStream writeStream;
                try
                {
                    writeStream = new FileStream(location, FileMode.Create);
                    BinaryWriter writeBinay = new BinaryWriter(writeStream);
                    writeBinay.Write(HostPath);
                    writeBinay.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Settings failed to save " + ex.ToString());
                }
            }
        }

        public static void Load()
        {
            String location = Path.Combine(Application.StartupPath, "settings.bin");
            if (File.Exists(location))
            {
                using (BinaryReader b = new BinaryReader(File.Open(location, FileMode.Open)))
                {
                    HostPath = b.ReadString();
                }
            }
        }

        public static String BaseApp =
            ";imports\n(import \"sys/lisp.inc\")\n(import \"class/lisp.inc\")\n(import \"gui/lisp.inc\")\n\n" +
            "(structure '+event 0\n" +
            "\t(byte 'close+ 'max+ 'min+))\n\n" +
            "(ui-window window ()\n" +
            "\t(ui-title-bar _ \"{0}\" (0xea19 0xea1b 0xea1a) +event_close+)\n" +
            "(defun-bind main ()\n" +
            "\t(bind '(x y w h) (apply view-locate (view-pref-size window)))\n" +
            "\t(gui-add (view-change window x y w h))\n" +
            "\t(while (cond\n" +
            "\t\t((= (defq id(get-long (defq msg (mail-read (task-mailbox))) ev_msg_target_id)) +event_close+)\n" +
            "\t\t\tnil)\n" +
            "\t\t((= id +event_close+)\n" +
            "\t\t\t;close button\n" +
            "\t\t\tnil)\n" +
            "\t\t((= id +event_min+)\n" +
            "\t\t\t;min button\n" +
            "\t\t\t(bind '(x y w h) (apply view-fit (cat (view-get-pos window) (view-pref-size window))))\n" +
            "\t\t\t(view-change-dirty window x y w h))\n" +
            "\t\t((= id +event_max+)\n" +
            "\t\t\t;max button\n" +
            "\t\t\t(bind '(x y w h) (apply view-fit (cat (view-get-pos window) '(512 512))))\n" +
            "\t\t\t(view-change-dirty window x y w h))\n" +
            "\t\t(t (view-event window msg))))\n" +
            "\t(view-hide window))\n";
    }
}
