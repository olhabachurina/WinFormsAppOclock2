namespace WinFormsAppOclock2
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer;
        private float angleHours;
        private float angleMinutes;
        private float angleSeconds;
        private Label timeLabel;
        public Form1()
        {
            InitializeComponent();
            InitializeClock(this);
            InitializeTimeLabel();
        }

        private void InitializeTimeLabel()
        {
            timeLabel = new Label();
            timeLabel.Font = new Font("Arial", 12);
            timeLabel.Location = new Point(10, 10);
            timeLabel.AutoSize = true;
            Controls.Add(timeLabel);
        }

        private void InitializeClock(Form1 form1)
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateClock();
            UpdateTimeLabel();
        }

        private void UpdateTimeLabel()
        {
            timeLabel.Text = "Current Time: " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void UpdateClock()
        {
            DateTime currentTime = DateTime.Now;
            int hours = currentTime.Hour;
            int minutes = currentTime.Minute;
            int seconds = currentTime.Second;
            angleHours = 360 / 12 * (hours % 12) + 360 / 12 * (minutes / 60.0f);
            angleMinutes = 360 / 60 * minutes + 360 / 60 * (seconds / 60.0f);
            angleSeconds = 360 / 60 * seconds;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawClockHands(g);
            base.OnPaint(e);
        }
        private void DrawClockHands(Graphics g)
        {
            float centerX = ClientSize.Width / 2.0f;
            float centerY = ClientSize.Height / 2.0f;
            float hourHandLength = 50;
            float minuteHandLength = 75;
            float secondHandLength = 90;
            PointF hourHandEnd = new PointF(centerX + hourHandLength * (float)Math.Cos((angleHours - 90) * (Math.PI / 180)), centerY + hourHandLength * (float)Math.Sin((angleHours - 90) * (Math.PI / 180)));
            PointF minuteHandEnd = new PointF(centerX + minuteHandLength * (float)Math.Cos((angleMinutes - 90) * (Math.PI / 180)), centerY + minuteHandLength * (float)Math.Sin((angleMinutes - 90) * (Math.PI / 180)));
            PointF secondHandEnd = new PointF(centerX + secondHandLength * (float)Math.Cos((angleSeconds - 90) * (Math.PI / 180)), centerY + secondHandLength * (float)Math.Sin((angleSeconds - 90) * (Math.PI / 180)));
            g.DrawLine(Pens.Black, centerX, centerY, hourHandEnd.X, hourHandEnd.Y);
            g.DrawLine(Pens.Black, centerX, centerY, minuteHandEnd.X, minuteHandEnd.Y);
            g.DrawLine(Pens.Red, centerX, centerY, secondHandEnd.X, secondHandEnd.Y);
        }
    }
}