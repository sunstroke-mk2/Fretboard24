namespace Fret24
{

    public partial class Form1 : Form
    {///                    0     1      2     3       4     5     6      7    8     9       10    11
        string[] notes = { "A", "A#/Bb", "B", "C", "C#/Db", "D", "D#/Eb", "E", "F", "F#/Gb", "G", "G#/Ab" };
        int g_fretsAmount = 22;
        int b_fretsAmount = 19;
        int[] gtuning = { 7, 2, 10, 5, 0, 7 };
        int[] btuning = { 10, 5,0,7 };
        Button[,] g_buttons;
        Button[,] b_buttons;
        string[] harmonic;
        string tonica;
        int  currentHarmonic=0;
        int[,] harmonics =
        {
            //major
            {0 ,2 ,4 ,5 ,7 ,9 ,11 ,12}, //ioniysk ,-Nmajor
            {0 ,2 ,4 ,6 ,7 ,9 ,11 ,12}, //lidiysk
            { 0 ,2 ,4 ,5 ,7 ,9 ,10 ,12}, //mixolid
            //minor
            { 0 ,2 ,3 ,5 ,7 ,8 ,10 ,12}, //Eoliysk ,-Nminor
            { 0 ,2 ,3 ,5 ,7 ,9 ,10 ,12}, //dorijsk
            { 0 ,1 ,3 ,5 ,7 ,8 ,10 ,12}, //Frigijsk
            { 0 ,2 ,3 ,5 ,7 ,8 ,10 ,12 }//Lokrijsk 
        };
         
        public Form1()
        {
            harmonic = new string[8];
            
            
            generateFretoards();
            InitializeComponent();
        }
        private void clearFretboard()
        {
            if (b_buttons != null)
                foreach (Button b in b_buttons)
                    this.Controls.Remove(b);

            if (g_buttons != null)
                foreach (Button b in g_buttons)
                    this.Controls.Remove(b);

            b_buttons = null;
            g_buttons = null;
           // MessageBox.Show("fretboard cleeaned");
        }
        private void generateFretoards()
        {
            clearFretboard();
            g_buttons = new Button[6, g_fretsAmount];
            b_buttons = new Button[4, b_fretsAmount];

            for (int s = 0; s < 6; s++)
                for (int f = 0; f < g_fretsAmount; f++)
                {
                    Button tmp = new Button();

                    int[] markedFrets = { 3, 5, 7, 9, 12, 15, 17, 19, 21 };
                    if (markedFrets.Contains(f))
                    {
                        tmp.FlatAppearance.BorderColor = Color.Black;
                        tmp.FlatAppearance.BorderSize = 3;
                        tmp.FlatStyle = FlatStyle.Flat;
                    }


                    tmp.Size = new Size(50, 30);
                    tmp.Location = new Point(f * 50, s * 30);
                    tmp.Text = notes[(gtuning[s] + f) % 12];
                    tmp.Click += new EventHandler(fretClick);
                    this.Controls.Add(tmp);
                    g_buttons[s, f] = tmp;


                }

            //all about bass
            for (int s = 0; s < 4; s++)
                for (int f = 0; f < b_fretsAmount; f++)
                {
                    Button tmp = new Button();


                    tmp.Size = new Size(50, 30);
                    tmp.Location = new Point(f * 50, 250 +s * 30);
                    tmp.Text = notes[(btuning[s] + f) % 12];
                    tmp.Click += new EventHandler(fretClick);
                    this.Controls.Add(tmp);
                    b_buttons[s, f] = tmp;


                }
        }

        private void fretClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            generateHarmonic(btn.Text);
            
        }
        private void generateHarmonic(string _tonica)
        {
            tonica = _tonica;
            int tonicaIndex=0;
            for(int i=0;i<notes.Length;i++)
            {
                if (_tonica == notes[i])
                    tonicaIndex = i;
            }
            for(int i=0;i<harmonic.Length;i++)
            {
                harmonic[i] = notes[(tonicaIndex + harmonics[currentHarmonic,i]) % 12];
            }

            foreach (Button b in g_buttons)
            {
                if (harmonic.Contains(b.Text))
                {
                    if (b.Text == harmonic[0])
                        b.BackColor = Color.Blue;
                    else
                        b.BackColor = Color.LightBlue;
                }
                else
                    b.BackColor = Color.White;
            }
            foreach (Button b in b_buttons)
            {
                if (harmonic.Contains(b.Text))
                {
                    if (b.Text == harmonic[0])
                        b.BackColor = Color.Gray;
                    else
                        b.BackColor = Color.LightGray;
                }
                else
                    b.BackColor = Color.White;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentHarmonic = listBox2.SelectedIndex;
            generateHarmonic(tonica);
        }

        //config button
        private void button2_Click(object sender, EventArgs e)
        {
            
                Form2 newForm = new Form2();
                newForm.Show();
                newForm.init(this);
            
        }

        public void getConfig(int bfrets,int gfrets, string[] btune,string[]gtune)
        {

            b_fretsAmount = bfrets;
            g_fretsAmount = gfrets;

          //  btuning = new int[] { 1, 1, 1, 1 };
          //  gtuning = new int[] { 2, 2, 2, 2, 2, 2 };
            
            for(int j=0;j<btune.Length;j++)
            {
                for(int i = 0; i < 12; i++)
                    if (btune[j] == notes[i])
                    {
                        btuning[j] = i;
                    }
            }
            for (int j = 0; j < gtune.Length; j++)
            {
                for (int i = 0; i < 12; i++)
                    if (gtune[j] == notes[i])
                    {
                        gtuning[j] = i;
                    }
            }
            
            generateFretoards();
        }
    }

}
