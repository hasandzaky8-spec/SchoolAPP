using System.Drawing.Text;

namespace SchoolApp
{
    /// <summary>
    /// Tema "Glacier" (biru es) + font Lato untuk seluruh aplikasi.
    /// </summary>
    internal static class Theme
    {
        // ===== Palet Glacier =====
        public static readonly Color Frost      = Color.FromArgb(0xEA, 0xF5, 0xF9); // latar form
        public static readonly Color Ice        = Color.FromArgb(0xF7, 0xFC, 0xFE); // input & tabel
        public static readonly Color IceAlt     = Color.FromArgb(0xE3, 0xF1, 0xF7); // baris selang-seling
        public static readonly Color Mist       = Color.FromArgb(0xBD, 0xE0, 0xEE); // tombol
        public static readonly Color MistHover  = Color.FromArgb(0xA5, 0xD3, 0xE5); // tombol (hover)
        public static readonly Color Glacier    = Color.FromArgb(0x2A, 0x6F, 0x8E); // warna utama (nav, label)
        public static readonly Color GlacierLt  = Color.FromArgb(0x3F, 0x86, 0xA6); // tombol nav non-aktif
        public static readonly Color Deep       = Color.FromArgb(0x12, 0x38, 0x4A); // teks
        public static readonly Color Border     = Color.FromArgb(0x9C, 0xC9, 0xDC); // garis
        public static readonly Color Selection  = Color.FromArgb(0x7D, 0xBA, 0xD3); // baris terpilih

        // ===== Font =====
        // Lato harus terpasang di Windows. Kalau belum, otomatis pakai Segoe UI.
        public const string PreferredFont = "Lato";
        public const string FallbackFont = "Segoe UI";

        public static readonly string FontName = ResolveFontName();

        private static string ResolveFontName()
        {
            using var fonts = new InstalledFontCollection();
            return fonts.Families.Any(f =>
                string.Equals(f.Name, PreferredFont, StringComparison.OrdinalIgnoreCase))
                ? PreferredFont
                : FallbackFont;
        }

        private static Font WithFamily(Font old) =>
            new Font(FontName, old.Size, old.Style, old.Unit);

        /// <summary>Terapkan tema ke form beserta seluruh kontrol di dalamnya.</summary>
        public static void Apply(Form form)
        {
            form.BackColor = Frost;
            form.ForeColor = Deep;
            ApplyTo(form);
        }

        private static void ApplyTo(Control parent)
        {
            parent.Font = WithFamily(parent.Font);

            foreach (Control c in parent.Controls)
            {
                switch (c)
                {
                    case DataGridView dgv:
                        StyleGrid(dgv);
                        break;

                    case Button btn:
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.FlatAppearance.MouseOverBackColor = MistHover;
                        btn.FlatAppearance.MouseDownBackColor = Border;
                        btn.BackColor = Mist;
                        btn.ForeColor = Deep;
                        btn.Cursor = Cursors.Hand;
                        break;

                    case TextBox txt:
                        txt.BackColor = Ice;
                        txt.ForeColor = Deep;
                        txt.BorderStyle = BorderStyle.FixedSingle;
                        break;

                    case Label lbl:
                        lbl.ForeColor = Glacier;
                        break;

                    case Panel pnl:
                        pnl.BackColor = pnl.Dock == DockStyle.Top ? Glacier : Frost;
                        break;
                }

                ApplyTo(c); // rekursif (font + anak kontrol)
            }
        }

        private static void StyleGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Ice;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Border;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;

            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Glacier;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Glacier;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font(FontName, dgv.Font.Size, FontStyle.Bold, dgv.Font.Unit);

            dgv.DefaultCellStyle.BackColor = Ice;
            dgv.DefaultCellStyle.ForeColor = Deep;
            dgv.DefaultCellStyle.SelectionBackColor = Selection;
            dgv.DefaultCellStyle.SelectionForeColor = Deep;
            dgv.DefaultCellStyle.Font =
                new Font(FontName, dgv.Font.Size, FontStyle.Regular, dgv.Font.Unit);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = IceAlt;
        }
    }
}
