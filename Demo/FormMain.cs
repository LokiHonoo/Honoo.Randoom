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
            description.AppendLine("����һ��ָ���ַ���Χ������ַ�����");
            description.AppendLine("���ؽ����ָ���ַ���Χ������ַ�����");
            description.AppendLine("�ַ������ǣ�");
            description.AppendLine("'d' ���������֣������������׻������ַ���");
            description.AppendLine("'D' ���������֡�");
            description.AppendLine("'a' Ӣ����ĸ�������������׻������ַ���");
            description.AppendLine("'A' Ӣ����ĸ��");
            description.AppendLine("'m' Ӣ����ĸ�Ͱ��������֣������������׻������ַ���");
            description.AppendLine("'M' Ӣ����ĸ�Ͱ��������֡�");
            description.AppendLine("'h' ʮ�������ַ���");
            description.AppendLine("'c' ʹ���Զ����ַ�������� '@' ָʾ��ͬʱʹ�á�");
            description.AppendLine("'@' ָʾ��֮����ַ���Ϊ�Զ����ַ�������� 'c' ָʾ��ͬʱʹ�á�");
            description.AppendLine("'+' ָʾ��֮�������ַ�ת��Ϊ��д��ʽ����Ӱ��ֱ�����ָʾ�� '!...!'��");
            description.AppendLine("'-' ָʾ��֮�������ַ�ת��ΪСд��ʽ����Ӱ��ֱ�����ָʾ�� '!...!'��");
            description.AppendLine("'!...!' ָʾ��֮�ڵ��ַ�ֱ�����������Ϊ�����ַ���");
            description.AppendLine("ʵ����");
            description.AppendLine("+mmmmm!-!mmmmm!-!mmmmm!-!mmmmm!-!mmmmm ģ�� Windows ���кš�");
            description.AppendLine("hhhhhhhh!-!hhhh!-!hhhh!-!hhhh!-!hhhhhhhhhhhh ģ�� GUID��");
            description.AppendLine("!WPD888-5!DDDD!-!DDDDD!-!DDDDD ģ�� Macromedia 8 ���кš�");
            description.AppendLine("cccccccccccccccccccccccc@ABCabc12345~!@#$%^* �Զ����ַ���");
            MessageBox.Show(description.ToString(), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.ComboBoxToken.Items.Add("'d' ���������֣������������׻������ַ���");
            this.ComboBoxToken.Items.Add("'D' ���������֡�");
            this.ComboBoxToken.Items.Add("'a' Ӣ����ĸ�������������׻������ַ���");
            this.ComboBoxToken.Items.Add("'A' Ӣ����ĸ��");
            this.ComboBoxToken.Items.Add("'m' Ӣ����ĸ�Ͱ��������֣������������׻������ַ���");
            this.ComboBoxToken.Items.Add("'M' Ӣ����ĸ�Ͱ��������֡�");
            this.ComboBoxToken.Items.Add("'h' ʮ�������ַ���");
            this.ComboBoxToken.SelectedIndex = 4;
            this.ComboBoxMark.Items.Add("+mmmmm!-!mmmmm!-!mmmmm!-!mmmmm!-!mmmmm");
            this.ComboBoxMark.Items.Add("hhhhhhhh!-!hhhh!-!hhhh!-!hhhh!-!hhhhhhhhhhhh");
            this.ComboBoxMark.Items.Add("!WPD888-5!DDDD!-!DDDDD!-!DDDDD");
            this.ComboBoxMark.Items.Add("cccccccccccccccccccccccc@ABCabc12345~!@#$%^*");
        }

        private void ListViewResult_ItemActivate(object sender, EventArgs e)
        {
            Clipboard.SetText((sender as ListView).SelectedItems[0].SubItems[1].Text);
        }
    }
}