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
         "abort :from-end :overwrite :adjustable :gensym :predicate :append :host " +
		":preserve-whitespace :array :if-does-not-exist :pretty :base :if-exists :print " +
		":case :include :print-function :circle :index :probe :conc-name :inherited " +
		":radix :constructor :initial-contents :read-only :copier :initial-element " +
		":rehash-size :count :initial-offset :rehash-threshold :create :initial-value " +
		":rename :default :input :rename-and-delete :defaults :internal :size :device " +
		":io :start :direction :junk-allowed :start1 :directory :key :start2 " +
		":displaced-index-offset :length :stream :displaced-to :level :supersede " +
		":element-type :name :test :end :named :test-not :end1 :new-version :type :end2 " +
		":nicknames :use :error :output :verbose :escape :output-file :version " +
		":external :documentation :shadowing-import-from :modern :export " +
		":case-sensitive :case-inverted :shadow :import-from :intern :fill-pointer " +
		":upcase :downcase :preserve :invert :load-toplevel :compile-toplevel :execute " +
		":while :until :for :do :if :then :else :when :unless :in :across :finally " +
		":collect :nconc :maximize :minimize :sum :and :with :initially :append :into " +
		":count :end :repeat :always :never :thereis :from :to :upto :downto :below " +
		":above :by :on :being :each :the :hash-key :hash-keys :hash-value :hash-values " +
		":using :of-type :upfrom :downfrom :arguments :return-type :library :full " +
		":malloc-free :none :alloca :in :out :in-out :stdc-stdcall :stdc :c :language " +
		":built-in :typedef :external :fini :init-once :init-always";

        public static String LispFunctions =
        "defun defun-bind def-func-end defmacro defcvar import defq defmacro-bind defmacro import cond structure ui-window window byte " +
        "def-struct def-struct-end def-func ffi each while not and or reduce setq when ui-title-bar bind gui-add view-hide view-event view-change-dirty ui-grid";

        public static String LispMacros =
        "rest zip empty? nempty? structure struct offset ptr long int short byte path write-short write-long write-line write-int write-char write" +
        " within-compile-env while empty? nempty? first second last when view-sub-opaque view-sub view-set-size view-fit view-set-pos view-set-flags" +
        " gui-info view-locate nil? aeach view-set-bounds view-pref-size view-layout view-hide view-get-size view-get-pos view-get-bounds view-find-id" +
        " view-event view-dirty-all view-dirty view-change-dirty view-change view-add-opaque view-add-front view-add-dirty view-add-child view-add-back" +
        " vdu-max-size vdu-load vdu-char-size unzip until unless undef ui-window ui-view ui-vdu ui-tree ui-tool-bar ui-title-bar ui-title ui-textfield" +
        " ui-slider ui-scroll ui-props ui-progress ui-merge-props ui-label ui-grid ui-flow ui-element hash ui-canvas ui-buttons ui-button ui-backdrop" +
        " type-to-size type-of tuple-set tuple-get trim-start trim-end trim to-upper to-lower times time throw task-sleep task-mailbox sym? seq? sym" +
        " swap string-stream intern-seq intern io-stream load-stream stream-flush stream-avail str? str-to-num str stdio-get-args starts-with sqrt split" +
        " sorted sort some! some slice sin sign shuffled shuffle setq setd set-field set save repl reduced-reduce reduced-reduce-rev reduced reduce-rev" +
        " reduce recip reals read-short read-long read-line read-int read-char read-avail read rcurry range random r2i r2f quote quasi-quote push progn" +
        " print prin pop pii-write-char pii-read-char pii-dirlist penv path-transform path-stroke-polylines path-stroke-polygons path-simplify" +
        " path-gen-quadratic path-gen-cubic path-gen-arc path-filter partition pad out-stream or opt open-remote open-pipe open-farm open-child nums-sum" +
        " nums-sub nums-scale nums-mul nums-mod nums-frac nums-floor nums-div nums-add nums-abs nums num? num-to-char ntz nto notevery notany not nlz nlo" +
        " neg min merge-obj mem-stats max match? map-rev map mail-send mail-select mail-read mail-poll mail-free-mbox mail-enquire mail-forget mail-declare" +
        " mail-alloc-mbox macroexpand lst? logxor lognot logior logand log2 load list let length lambda macro unquote-splicing num-to-utf8 unquote mail-devices" +
        " join debug-fun? debug-send insert inc in-stream in-set-state in-next-msg in-mbox in-get-state import if i2r i2f gui-add-back gui-add get-ushort" +
        " get-uint get-ubyte get-short get-long get-int get-field get-cstr get-byte get gensym frac font-glyph-ranges font-glyph-paths fnc? floor fixeds" +
        " find-rev find filter file-stream ffi f2r f2i exec every eval erase eql env ends-with elem-set elem each! each-rev each-mergeable-rev each-mergeable" +
        " each-line each defun-debug defun-bind defun defq defmacro-bind defmacro def dec debug-fun debug-format debug curry create-window create-view" +
        " create-vdu create-title create-textfield create-stdio create-slider create-scroll create-progress create-label create-grid create-font create-flow" +
        " create-canvas create-button create-backdrop cpu cos copy const cond compose component-get-id component-connect code cmp clear char-to-num char catch" +
        " cat case cap canvas-swap canvas-set-flags canvas-set-color canvas-save canvas-resize canvas-plot canvas-next-frame seq? canvas-load canvas-info" +
        " canvas-fpoly canvas-fill reverse canvas-fbox prebind bind awhile awhen ascii-upper ascii-lower ascii-code ascii-char tolist array apply and align" +
        " aif age acond abs abi aand >>> >> >= > = <= << < + % /= / * -";

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
