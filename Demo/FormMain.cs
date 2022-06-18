using System.Text;

namespace Demo
{
    public partial class FormMain : Form
    {
        private Honoo.Randoom _randoom = new Honoo.Randoom();

        public FormMain()
        {
            InitializeComponent();
        }

        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            this.ListViewResult.Items.Clear();
            if (RadioButtonMark.Checked)
            {
                string mark = this.ComboBoxMark.Text;
                for (int i = 0; i < this.NumericUpDownGenerateCount.Value; i++)
                {
                    try
                    {
                        this.ListViewResult.Items.Add(new ListViewItem(new string[] { i.ToString(), _randoom.NextString(mark) }));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                char token;
                switch (this.ComboBoxToken.SelectedIndex)
                {
                    case 6: token = 'h'; break;
                    case 5: token = 'M'; break;
                    case 4: token = 'm'; break;
                    case 3: token = 'A'; break;
                    case 2: token = 'a'; break;
                    case 1: token = 'D'; break;
                    case 0: token = 'd'; break;
                    default: token = 'm'; break;
                }
                int charCount = (int)this.NumericUpDownCharCount.Value;
                for (int i = 0; i < this.NumericUpDownGenerateCount.Value; i++)
                {
                    this.ListViewResult.Items.Add(new ListViewItem(new string[] { i.ToString(), _randoom.NextString(token, charCount) }));
                }
            }
        }

        private void ButtonMarkDescription_Click(object sender, EventArgs e)
        {
            StringBuilder description = new StringBuilder();
            description.AppendLine("返回一个指定字符范围的随机字符串。");
            description.AppendLine("返回结果：指定字符范围的随机字符串。");
            description.AppendLine("字符掩码标记：");
            description.AppendLine("'d' 阿拉伯数字，不包括字形易混淆的字符。");
            description.AppendLine("'D' 阿拉伯数字。");
            description.AppendLine("'a' 大写和小写英文字母，不包括字形易混淆的字符。");
            description.AppendLine("'A' 大写和小写英文字母。");
            description.AppendLine("'m' 大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符。");
            description.AppendLine("'M' 大写和小写英文字母和阿拉伯数字。");
            description.AppendLine("'h' 小写十六进制字符。");
            description.AppendLine("'c' 使用自定义字符。需配合 '@' 指示符同时使用。");
            description.AppendLine("'@' 指示符之后的字符作为自定义字符。需配合 'c' 指示符同时使用。");
            description.AppendLine("'+' 指示符之后的随机字符转换为大写形式。不影响直接输出指示符 '(...)'。");
            description.AppendLine("'-' 指示符之后的随机字符转换为小写形式。不影响直接输出指示符 '(...)'。");
            description.AppendLine("'.' 指示符之后的随机字符不再进行大小写转换。");
            description.AppendLine("'(...)' 指示符之内的字符直接输出，不作为掩码字符。");
            description.AppendLine("'(..!)..)' '!'后一个字符直接输出，主要用于后括号 ')' 输出。");
            description.AppendLine("'[number]' 指示符之内的数字表示输出前一随机字符的个数。");
            description.AppendLine("实例：");
            description.AppendLine("+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm 模拟 Windows 序列号。");
            description.AppendLine("h[8](-)h[4](-)h[4](-)h[4](-)h[12] 模拟 GUID。");
            description.AppendLine("(WPD888-5)DDDD(-)DDDDD(-)DDDDD 模拟 Macromedia 8 序列号。");
            description.AppendLine("(AAA)cccccc(---)c[12]@ABCabc12345~!@#$%^* 自定义字符。");
            MessageBox.Show(description.ToString(), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.ComboBoxToken.Items.Add("'d' 阿拉伯数字，不包括字形易混淆的字符。");
            this.ComboBoxToken.Items.Add("'D' 阿拉伯数字。");
            this.ComboBoxToken.Items.Add("'a' 大写和小写英文字母，不包括字形易混淆的字符。");
            this.ComboBoxToken.Items.Add("'A' 大写和小写英文字母。");
            this.ComboBoxToken.Items.Add("'m' 大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符。");
            this.ComboBoxToken.Items.Add("'M' 大写和小写英文字母和阿拉伯数字。");
            this.ComboBoxToken.Items.Add("'h' 小写十六进制字符。");
            this.ComboBoxToken.SelectedIndex = 4;
            this.ComboBoxMark.Items.Add("+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm");
            this.ComboBoxMark.Items.Add("h[8](-)h[4](-)h[4](-)h[4](-)h[12]");
            this.ComboBoxMark.Items.Add("(WPD888-5)DDDD(-)DDDDD(-)DDDDD");
            this.ComboBoxMark.Items.Add("(AAA)cccccc(---)c[12]@ABCabc12345~!@#$%^*");
        }

        private void ListViewResult_ItemActivate(object sender, EventArgs e)
        {
            Clipboard.SetText((sender as ListView)!.SelectedItems[0].SubItems[1].Text);
        }
    }
}