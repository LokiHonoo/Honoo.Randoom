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
            description.AppendLine("�ַ���Χ��ǺͿ��Ʒ���");
            description.AppendLine("'d' ���������֣������������׻������ַ���");
            description.AppendLine("'D' ���������֡�");
            description.AppendLine("'1' ���������֣������������׻������ַ���'d' �ı�����");
            description.AppendLine("'0' ���������֡�'D' �ı�����");
            description.AppendLine("'a' ��д��СдӢ����ĸ�������������׻������ַ���");
            description.AppendLine("'A' ��д��СдӢ����ĸ��");
            description.AppendLine("'m' ��д��СдӢ����ĸ�Ͱ��������֣������������׻������ַ���");
            description.AppendLine("'M' ��д��СдӢ����ĸ�Ͱ��������֡�");
            description.AppendLine("'h' Сдʮ�������ַ���");
            description.AppendLine("'c' ʹ���Զ����ַ����ϡ������ '@' ���Ʒ�ͬʱʹ�á�");
            description.AppendLine("'@' ���Ʒ�֮����ַ���Ϊ�Զ����ַ�������� 'c' ���ͬʱʹ�á�");
            description.AppendLine("'+' ���Ʒ�֮�������ַ�ת��Ϊ��д��ʽ����Ӱ��ֱ��������Ʒ� '(...)'��");
            description.AppendLine("'-' ���Ʒ�֮�������ַ�ת��ΪСд��ʽ����Ӱ��ֱ��������Ʒ� '(...)'��");
            description.AppendLine("'.' ���Ʒ�֮�������ַ����ٽ��д�Сдת����");
            description.AppendLine("'(...)' ���Ʒ�֮�ڵ��ַ�ֱ�����������Ϊ�����ַ���");
            description.AppendLine("'(..!)..)' '!'��һ���ַ�ֱ���������Ҫ���ں����� ')' �����");
            description.AppendLine("'[number]' ���Ʒ�֮�ڵ����ֱ�ʾ���ǰһ����ַ��ĸ�����");
            description.AppendLine();
            description.AppendLine("ʵ����");
            description.AppendLine("+mmmmm(-)mmmmm(-)mmmmm(-)mmmmm(-)mmmmm ģ�� Windows ���кš�");
            description.AppendLine("h[8](-)h[4](-)h[4](-)h[4](-)h[12] ģ�� GUID��");
            description.AppendLine("(WPD888-5)DDDD(-)DDDDD(-)DDDDD ģ�� Macromedia 8 ���кš�");
            description.AppendLine("ccccccccccccccccccccccccc@ABCabc12345~!@#$%^* �Զ����ַ���");
            MessageBox.Show(description.ToString(), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.ComboBoxToken.Items.Add("'d' ���������֣������������׻������ַ���");
            this.ComboBoxToken.Items.Add("'D' ���������֡�");
            this.ComboBoxToken.Items.Add("'a' ��д��СдӢ����ĸ�������������׻������ַ���");
            this.ComboBoxToken.Items.Add("'A' ��д��СдӢ����ĸ��");
            this.ComboBoxToken.Items.Add("'m' ��д��СдӢ����ĸ�Ͱ��������֣������������׻������ַ���");
            this.ComboBoxToken.Items.Add("'M' ��д��СдӢ����ĸ�Ͱ��������֡�");
            this.ComboBoxToken.Items.Add("'h' Сдʮ�������ַ���");
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