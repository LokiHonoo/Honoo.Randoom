using System;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
    public partial class FormMain : Form
    {
        private readonly Honoo.Randoom _randoom = new();

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
                var token = this.ComboBoxToken.SelectedIndex switch
                {
                    6 => 'h',
                    5 => 'M',
                    4 => 'm',
                    3 => 'A',
                    2 => 'a',
                    1 => 'D',
                    0 => 'd',
                    _ => 'm',
                };
                int charCount = (int)this.NumericUpDownCharCount.Value;
                for (int i = 0; i < this.NumericUpDownGenerateCount.Value; i++)
                {
                    this.ListViewResult.Items.Add(new ListViewItem(new string[] { i.ToString(), _randoom.NextString(charCount, token) }));
                }
            }
        }

        private void ButtonMarkDescription_Click(object sender, EventArgs e)
        {
            var description = new StringBuilder();
            description.AppendLine("字符范围标记和控制符：");
            description.AppendLine("'d' 阿拉伯数字，不包括字形易混淆的字符。");
            description.AppendLine("'D' 阿拉伯数字。");
            description.AppendLine("'1' 阿拉伯数字，不包括字形易混淆的字符。'd' 的别名。");
            description.AppendLine("'0' 阿拉伯数字。'D' 的别名。");
            description.AppendLine("'a' 大写和小写英文字母，不包括字形易混淆的字符。");
            description.AppendLine("'A' 大写和小写英文字母。");
            description.AppendLine("'m' 大写和小写英文字母和阿拉伯数字，不包括字形易混淆的字符。");
            description.AppendLine("'M' 大写和小写英文字母和阿拉伯数字。");
            description.AppendLine("'h' 小写十六进制字符。");
            description.AppendLine("'c' 使用自定义字符集合。需配合 '@' 控制符同时使用。");
            description.AppendLine("'@' 控制符之后的字符作为自定义字符。需配合 'c' 标记同时使用。");
            description.AppendLine("'+' 控制符之后的随机字符转换为大写形式。不影响直接输出控制符 '(...)'。");
            description.AppendLine("'-' 控制符之后的随机字符转换为小写形式。不影响直接输出控制符 '(...)'。");
            description.AppendLine("'.' 控制符之后的随机字符不再进行大小写转换。");
            description.AppendLine("'(...)' 控制符之内的字符直接输出，不作为掩码字符。");
            description.AppendLine("'(..!)..)' '!'后一个字符直接输出，主要用于后括号 ')' 输出。");
            description.AppendLine("'[number]' 控制符之内的数字表示输出前一随机字符的个数。");
            description.AppendLine();
            description.AppendLine("实例：");
            description.AppendLine("+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm 模拟 Windows 序列号。");
            description.AppendLine("h[8](-)h[4](-)h[4](-)h[4](-)h[12] 模拟 GUID。");
            description.AppendLine("(WPD888-5)DDDD(-)DDDDD(-)DDDDD 模拟 Macromedia 8 序列号。");
            description.AppendLine("ccccccccccccccccccccccccc@ABCabc12345~!@#$%^* 自定义字符。");
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
            this.ComboBoxMark.Items.Add("ccccccccccccccccccccccccc@ABCabc12345~!@#$%^*");
        }

        private void ListViewResult_ItemActivate(object sender, EventArgs e)
        {
            Clipboard.SetText((sender as ListView)!.SelectedItems[0].SubItems[1].Text);
        }
    }
}