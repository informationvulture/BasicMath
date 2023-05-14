using System;
using System.Drawing;
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
        private Label answerLabel1 = new Label();
        private Label answerLabel2 = new Label();
        private Label answerLabel3 = new Label();
        private Label answerLabel4 = new Label();
        private Label resultLabel = new Label();
        private Label scoreLabel = new Label();
        private Label timerLabel = new Label();
        private Timer timer = new Timer();
        private Button nextButton = new Button();
        private DateTime startTime;
        private int correctAnswers = 0;
        private int incorrectAnswers = 0;

        public MultiplicationForm()
        {
            Size = new Size(800, 600);
            Icon = Properties.Resources.app_icon_122;
            questionLabel.Location = new Point(10, 10);
            questionLabel.Width = 400;
            Controls.Add(questionLabel);

            answerLabel1.Location = new Point(10, 40);
            answerLabel1.Click += new EventHandler(AnswerLabel_Click);
            Controls.Add(answerLabel1);

            answerLabel2.Location = new Point(10, 70);
            answerLabel2.Click += new EventHandler(AnswerLabel_Click);
            Controls.Add(answerLabel2);

            answerLabel3.Location = new Point(10, 100);
            answerLabel3.Click += new EventHandler(AnswerLabel_Click);
            Controls.Add(answerLabel3);

            answerLabel4.Location = new Point(10, 130);
            answerLabel4.Click += new EventHandler(AnswerLabel_Click);
            Controls.Add(answerLabel4);

            resultLabel.Location = new Point(10, 160);
            resultLabel.Width = 400;
            Controls.Add(resultLabel);

            scoreLabel.Location = new Point(10, 190);
            scoreLabel.Width = 400;
            Controls.Add(scoreLabel);

            timerLabel.Location = new Point(10, 220);
            timerLabel.Width = 400;
            Controls.Add(timerLabel);

            nextButton.Text = "Next";
            nextButton.Location = new Point(10, 250);
            nextButton.Click += new EventHandler(NextButton_Click);
            Controls.Add(nextButton);
            nextButton.Visible = false;

            timer.Interval = 1000; // one second
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();

            startTime = DateTime.Now;

            GenerateNewProblem();
        }

        private void GenerateNewProblem()
        {
            nextButton.Visible = false;
            int number1 = random.Next(100, 1000);
            int number2 = random.Next(10, 100);
            int correctAnswer = number1 * number2;

            questionLabel.Text = $"Please multiply these two numbers: {number1} and {number2}";
            questionLabel.Tag = correctAnswer;

            // Generate random answers
            int[] answers = new int[4];
            answers[0] = correctAnswer;
            for (int i = 1; i < 4; i++)
            {
                answers[i] = random.Next(1000, 100000);
            }

            // Shuffle the answers
            for (int i = answers.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                int temp = answers[i];
                answers[i] = answers[j];
                answers[j] = temp;
            }

                answerLabel1.Text = answers[0].ToString();
                answerLabel1.Tag = answers[0] == correctAnswer;

                answerLabel2.Text = answers[1].ToString();
                answerLabel2.Tag = answers[1] == correctAnswer;

                answerLabel3.Text = answers[2].ToString();
                answerLabel3.Tag = answers[2] == correctAnswer;

                answerLabel4.Text = answers[3].ToString();
                answerLabel4.Tag = answers[3] == correctAnswer;

                resultLabel.Text = string.Empty;
            }

    private void AnswerLabel_Click(object sender, EventArgs e)
            {
                Label clickedLabel = (Label)sender;
                if ((bool)clickedLabel.Tag)
                {
                    resultLabel.Text = "Correct! Well done.";
                    correctAnswers++;
                }
                else
                {
                    resultLabel.Text = $"Sorry, that's incorrect. The correct answer is {(int)questionLabel.Tag}.";
                    incorrectAnswers++;
                    nextButton.Visible = true;
                }
                scoreLabel.Text = $"Score: {correctAnswers} correct, {incorrectAnswers} incorrect";
            }

            private void NextButton_Click(object sender, EventArgs e)
            {
                GenerateNewProblem();
            }

            private void Timer_Tick(object sender, EventArgs e)
            {
                TimeSpan elapsed = DateTime.Now - startTime;
                timerLabel.Text = $"Elapsed time: {elapsed.ToString(@"hh\:mm\:ss")}";
            }
        }
    }
