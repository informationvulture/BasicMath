using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicMath
{



    public class Program
    {
        static void Main()
        {
            Application.Run(new MultiplicationForm());
        }
    }



    public class MultiplicationForm : Form
    {
        private Random random = new Random();
        private Label questionLabel = new Label();
        private TextBox answerTextBox = new TextBox();
        private Button submitButton = new Button();
        private Label resultLabel = new Label();
        private Label timerLabel = new Label();
        private Timer timer = new Timer();
        private DateTime startTime;
        private Label correctLabel = new Label();
        private Label incorrectLabel = new Label();
        private int correctScore = 0;
        private int incorrectScore = 0;


        public MultiplicationForm()
        {
            this.Icon = Properties.Resources.app_icon_122;
            questionLabel.Location = new Point(10, 10);
            questionLabel.Width = 400;
            Controls.Add(questionLabel);

            answerTextBox.Location = new Point(10, 40);
            Controls.Add(answerTextBox);

            submitButton.Text = "Submit";
            submitButton.Location = new Point(10, 70);
            submitButton.Click += new EventHandler(SubmitButton_Click);
            Controls.Add(submitButton);

            resultLabel.Location = new Point(10, 100);
            resultLabel.Width = 400;
            Controls.Add(resultLabel);

            timerLabel.Location = new Point(10, 130);
            timerLabel.Width = 400;
            Controls.Add(timerLabel);

            timer.Interval = 1000; // one second
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();

            startTime = DateTime.Now;

            correctLabel.Location = new Point(10, 160);
            correctLabel.Width = 200;
            Controls.Add(correctLabel);

            incorrectLabel.Location = new Point(10, 190);
            incorrectLabel.Width = 200;
            Controls.Add(incorrectLabel);

            UpdateScores();

            GenerateNewProblem();


        }

        private void UpdateScores()
        {
            correctLabel.Text = $"Correct answers: {correctScore}";
            incorrectLabel.Text = $"Incorrect answers: {incorrectScore}";
        }
        private void GenerateNewProblem()
        {
            int number1 = random.Next(100, 1000);
            int number2 = random.Next(10, 100);

            questionLabel.Text = $"Please multiply these two numbers: {number1} and {number2}";
            questionLabel.Tag = number1 * number2;

            answerTextBox.Text = string.Empty;
            resultLabel.Text = string.Empty;
        }

        private async void SubmitButton_Click(object sender, EventArgs e)
        {
            int userAnswer;
            if (int.TryParse(answerTextBox.Text, out userAnswer))
            {
                if ((int)questionLabel.Tag == userAnswer)
                {
                    correctScore++;
                    UpdateScores();
                    GenerateNewProblem();
                }
                else
                {
                    resultLabel.Text = $"Sorry, that's incorrect. The correct answer is {(int)questionLabel.Tag}.";
                    await Task.Delay(2000);
                    incorrectScore++;
                    UpdateScores();
                    GenerateNewProblem();
                }
            }
            else
            {
                resultLabel.Text = "Invalid input. Please enter a number.";
            }


        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            timerLabel.Text = $"Elapsed time: {elapsed.ToString(@"hh\:mm\:ss")}";
        }
    }



}
