using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SzkolnyQuiz
{
    public class Question
    {
        public string Category { get; set; }
        public string QuestionText { get; set; }
        public string[] Options { get; set; }
        public int CorrectIndex { get; set; }

        public Question(string category, string questionText, string[] options, int correctIndex)
        {
            Category = category;
            QuestionText = questionText;
            Options = options;
            CorrectIndex = correctIndex;
        }
    }

    public class UserAnswer
    {
        public Question QuestionData { get; set; }
        public int ChosenOption { get; set; }
        public int CorrectOption { get; set; }
        public bool IsCorrect { get; set; }

        public UserAnswer(Question q, int chosen, int correct)
        {
            QuestionData = q;
            ChosenOption = chosen;
            CorrectOption = correct;
            IsCorrect = (chosen == correct);
        }
    }

    public class QuizForm : Form
    {
        private readonly Color ColorBg = Color.FromArgb(15, 23, 42);
        private readonly Color ColorCard = Color.FromArgb(30, 41, 59);
        private readonly Color ColorCardBorder = Color.FromArgb(51, 65, 85);
        private readonly Color ColorTextMain = Color.FromArgb(248, 250, 252);
        private readonly Color ColorTextMuted = Color.FromArgb(148, 163, 184);
        private readonly Color ColorAccent = Color.FromArgb(99, 102, 241);
        private readonly Color ColorBtnBg = Color.FromArgb(51, 65, 85);
        private readonly Color ColorBtnHover = Color.FromArgb(71, 85, 105);
        private readonly Color ColorBtnSelected = Color.FromArgb(67, 56, 202);
        private readonly Color ColorSuccess = Color.FromArgb(34, 197, 94);
        private readonly Color ColorDanger = Color.FromArgb(239, 68, 68);
        private readonly Color ColorBadgeBg = Color.FromArgb(49, 46, 129);
        private readonly Color ColorBadgeText = Color.FromArgb(199, 210, 254);

        private List<Question> questionPool = new List<Question>();
        private List<Question> currentQuestions = new List<Question>();
        private List<UserAnswer> userAnswers = new List<UserAnswer>();
        private Random rng = new Random();

        private int currentIndex = 0;
        private int selectedOption = -1;

        private Panel mainContainer;
        private Label titleLbl;
        private Label counterLbl;
        private Panel progressBar;
        private Panel cardPanel;
        private Label badgeLbl;
        private Label questionLbl;
        private List<Panel> optionPanels = new List<Panel>();
        private List<Label> optionBadges = new List<Label>();
        private List<Label> optionTexts = new List<Label>();
        private Label errorLbl;
        private Button nextBtn;

        public QuizForm()
        {
            InitializeData();
            PickRandomQuestions();
            InitializeWindow();
            ShowQuestionScreen();
        }

        private void InitializeData()
        {
            questionPool.Add(new Question("🌍 Geografia", "Jaka jest oficjalna stolica Australii?", new string[] { "Sydney", "Melbourne", "Canberra", "Brisbane" }, 2));
            questionPool.Add(new Question("🌍 Geografia", "Jaka jest najdłuższa rzeka w Polsce?", new string[] { "Odra", "Wisła", "Warta", "Bug" }, 1));
            questionPool.Add(new Question("🌍 Geografia", "Na jakim kontynencie znajduje się pustynia Sahara?", new string[] { "Azja", "Ameryka Południowa", "Australia", "Afryka" }, 3));
            questionPool.Add(new Question("⚔️ Historia", "W którym roku miała miejsce słynna bitwa pod Grunwaldem?", new string[] { "1385 r.", "1410 r.", "1466 r.", "1525 r." }, 1));
            questionPool.Add(new Question("⚔️ Historia", "W którym roku odbył się chrzest Polski?", new string[] { "966 r.", "1000 r.", "1025 r.", "1138 r." }, 0));
            questionPool.Add(new Question("⚔️ Historia", "Kto był pierwszym koronowanym królem Polski?", new string[] { "Mieszko I", "Bolesław Chrobry", "Kazimierz Wielki", "Władysław Łokietek" }, 1));
            questionPool.Add(new Question("🧬 Biologia", "Który organ w ciele człowieka odpowiada za produkcję insuliny?", new string[] { "Wątroba", "Trzustka", "Żołądek", "Nerki" }, 1));
            questionPool.Add(new Question("🧬 Biologia", "Jak nazywa się podstawowa jednostka strukturalna organizmów żywych?", new string[] { "Tkanka", "Komórka", "Narząd", "Białko" }, 1));
            questionPool.Add(new Question("🧬 Biologia", "Który barwnik w roślinach odpowiada za fotosyntezę?", new string[] { "Melanina", "Karoten", "Chlorofil", "Hemoglobina" }, 2));
            questionPool.Add(new Question("⚗️ Chemia", "Jaki jest symbol chemiczny złota w układzie okresowym pierwiastków?", new string[] { "Ag", "Fe", "Au", "Pt" }, 2));
            questionPool.Add(new Question("⚗️ Chemia", "Jaki odczyn ma roztwór o pH równym dokładnie 7?", new string[] { "Kwasowy", "Zasadowy", "Obojętny", "Żrący" }, 2));
            questionPool.Add(new Question("⚗️ Chemia", "Jaki jest wzór chemiczny wody?", new string[] { "CO2", "H2O", "NaCl", "O2" }, 1));
            questionPool.Add(new Question("📐 Matematyka", "Ile wynosi wynik działania: 2 + 2 × 2?", new string[] { "6", "8", "4", "10" }, 0));
            questionPool.Add(new Question("📐 Matematyka", "Ile wynosi suma kątów w dowolnym trójkącie?", new string[] { "90°", "180°", "270°", "360°" }, 1));
            questionPool.Add(new Question("📐 Matematyka", "Ile wynosi pierwiastek kwadratowy z liczby 144?", new string[] { "11", "12", "13", "14" }, 1));
            questionPool.Add(new Question("⚡ Fizyka", "W jakiej jednostce w układzie SI mierzymy siłę?", new string[] { "Dżul (J)", "Wat (W)", "Niuton (N)", "Paskal (Pa)" }, 2));
            questionPool.Add(new Question("💻 Informatyka", "Ile bitów mieści się w jednym bajcie (1 B)?", new string[] { "4 bity", "8 bitów", "16 bitów", "32 bity" }, 1));
            questionPool.Add(new Question("📚 Język Polski", "Kto napisał epopeję narodową „Pan Tadeusz”?", new string[] { "Juliusz Słowacki", "Henryk Sienkiewicz", "Adam Mickiewicz", "Bolesław Prus" }, 2));
        }

        private void PickRandomQuestions()
        {
            List<Question> copy = new List<Question>(questionPool);
            currentQuestions = new List<Question>();

            int count = Math.Min(5, copy.Count);
            for (int i = 0; i < count; i++)
            {
                int rIdx = rng.Next(copy.Count);
                currentQuestions.Add(copy[rIdx]);
                copy.RemoveAt(rIdx);
            }
        }

        private void InitializeWindow()
        {
            this.Text = "Szkolny Quiz Wiedzy - Losowe Pytania C#";
            this.Size = new Size(760, 680);
            this.MinimumSize = new Size(720, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorBg;
            this.DoubleBuffered = true;

            mainContainer = new Panel();
            mainContainer.Dock = DockStyle.Fill;
            mainContainer.Padding = new Padding(25);
            mainContainer.BackColor = ColorBg;
            this.Controls.Add(mainContainer);
        }

        private void ShowQuestionScreen()
        {
            mainContainer.Controls.Clear();
            selectedOption = -1;

            Question q = currentQuestions[currentIndex];
            int total = currentQuestions.Count;

            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 70;
            headerPanel.BackColor = ColorBg;

            titleLbl = new Label();
            titleLbl.Text = "🎓 SZKOLNY QUIZ WIEDZY";
            titleLbl.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titleLbl.ForeColor = ColorTextMain;
            titleLbl.AutoSize = true;
            titleLbl.Location = new Point(0, 5);
            headerPanel.Controls.Add(titleLbl);

            counterLbl = new Label();
            counterLbl.Text = string.Format("Pytanie {0} z {1}", currentIndex + 1, total);
            counterLbl.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            counterLbl.ForeColor = ColorAccent;
            counterLbl.AutoSize = true;
            counterLbl.Location = new Point(mainContainer.Width - 200, 5);
            counterLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            headerPanel.Controls.Add(counterLbl);

            Panel progressBg = new Panel();
            progressBg.Height = 8;
            progressBg.Dock = DockStyle.Bottom;
            progressBg.BackColor = ColorCard;

            progressBar = new Panel();
            int fillWidth = (int)(((double)(currentIndex + 1) / total) * (mainContainer.Width - 50));
            progressBar.Size = new Size(fillWidth, 8);
            progressBar.BackColor = ColorAccent;
            progressBg.Controls.Add(progressBar);

            headerPanel.Controls.Add(progressBg);
            mainContainer.Controls.Add(headerPanel);

            cardPanel = new Panel();
            cardPanel.Dock = DockStyle.Fill;
            cardPanel.BackColor = ColorCard;
            cardPanel.Padding = new Padding(24);
            cardPanel.Margin = new Padding(0, 15, 0, 15);

            badgeLbl = new Label();
            badgeLbl.Text = "  " + q.Category + "  ";
            badgeLbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            badgeLbl.ForeColor = ColorBadgeText;
            badgeLbl.BackColor = ColorBadgeBg;
            badgeLbl.AutoSize = true;
            badgeLbl.Location = new Point(24, 20);
            cardPanel.Controls.Add(badgeLbl);

            questionLbl = new Label();
            questionLbl.Text = q.QuestionText;
            questionLbl.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            questionLbl.ForeColor = ColorTextMain;
            questionLbl.Location = new Point(24, 55);
            questionLbl.Size = new Size(mainContainer.Width - 100, 50);
            questionLbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cardPanel.Controls.Add(questionLbl);

            optionPanels.Clear();
            optionBadges.Clear();
            optionTexts.Clear();
            string[] letters = { "A", "B", "C", "D" };

            int startY = 115;
            for (int i = 0; i < q.Options.Length; i++)
            {
                int optIdx = i;
                Panel optBox = new Panel();
                optBox.Location = new Point(24, startY + (i * 55));
                optBox.Size = new Size(mainContainer.Width - 100, 46);
                optBox.BackColor = ColorBtnBg;
                optBox.Cursor = Cursors.Hand;
                optBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                Label letBadge = new Label();
                letBadge.Text = letters[i];
                letBadge.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                letBadge.ForeColor = ColorTextMain;
                letBadge.BackColor = Color.FromArgb(71, 85, 105);
                letBadge.TextAlign = ContentAlignment.MiddleCenter;
                letBadge.Size = new Size(36, 46);
                letBadge.Location = new Point(0, 0);
                optBox.Controls.Add(letBadge);

                Label txtLbl = new Label();
                txtLbl.Text = q.Options[i];
                txtLbl.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                txtLbl.ForeColor = ColorTextMain;
                txtLbl.TextAlign = ContentAlignment.MiddleLeft;
                txtLbl.Location = new Point(48, 0);
                txtLbl.Size = new Size(optBox.Width - 55, 46);
                txtLbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                optBox.Controls.Add(txtLbl);

                EventHandler clickHandler = (s, e) => SelectOption(optIdx);
                EventHandler enterHandler = (s, e) => {
                    if (selectedOption != optIdx) optBox.BackColor = ColorBtnHover;
                };
                EventHandler leaveHandler = (s, e) => {
                    if (selectedOption != optIdx) optBox.BackColor = ColorBtnBg;
                };

                optBox.Click += clickHandler;
                letBadge.Click += clickHandler;
                txtLbl.Click += clickHandler;

                optBox.MouseEnter += enterHandler;
                txtLbl.MouseEnter += enterHandler;
                optBox.MouseLeave += leaveHandler;
                txtLbl.MouseLeave += leaveHandler;

                optionPanels.Add(optBox);
                optionBadges.Add(letBadge);
                optionTexts.Add(txtLbl);
                cardPanel.Controls.Add(optBox);
            }

            errorLbl = new Label();
            errorLbl.Text = "";
            errorLbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            errorLbl.ForeColor = ColorDanger;
            errorLbl.AutoSize = true;
            errorLbl.Location = new Point(24, startY + (4 * 55) + 10);
            cardPanel.Controls.Add(errorLbl);

            mainContainer.Controls.Add(cardPanel);

            Panel footerPanel = new Panel();
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Height = 60;
            footerPanel.BackColor = ColorBg;

            nextBtn = new Button();
            nextBtn.Text = (currentIndex < total - 1) ? "Następne pytanie ➔" : "Zobacz wynik 🏁";
            nextBtn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            nextBtn.BackColor = ColorAccent;
            nextBtn.ForeColor = Color.White;
            nextBtn.FlatStyle = FlatStyle.Flat;
            nextBtn.FlatAppearance.BorderSize = 0;
            nextBtn.Size = new Size(200, 44);
            nextBtn.Location = new Point(mainContainer.Width - 250, 10);
            nextBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            nextBtn.Cursor = Cursors.Hand;
            nextBtn.Click += (s, e) => SubmitAnswer();
            footerPanel.Controls.Add(nextBtn);

            mainContainer.Controls.Add(footerPanel);
        }

        private void SelectOption(int idx)
        {
            selectedOption = idx;
            errorLbl.Text = "";

            for (int i = 0; i < optionPanels.Count; i++)
            {
                if (i == idx)
                {
                    optionPanels[i].BackColor = ColorBtnSelected;
                    optionBadges[i].BackColor = ColorAccent;
                }
                else
                {
                    optionPanels[i].BackColor = ColorBtnBg;
                    optionBadges[i].BackColor = Color.FromArgb(71, 85, 105);
                }
            }
        }

        private void SubmitAnswer()
        {
            if (selectedOption == -1)
            {
                errorLbl.Text = "⚠️ Proszę zaznaczyć jedną z odpowiedzi przed przejściem dalej!";
                return;
            }

            Question q = currentQuestions[currentIndex];
            userAnswers.Add(new UserAnswer(q, selectedOption, q.CorrectIndex));

            currentIndex++;
            if (currentIndex < currentQuestions.Count)
            {
                ShowQuestionScreen();
            }
            else
            {
                ShowResultsScreen();
            }
        }

        private void ShowResultsScreen()
        {
            mainContainer.Controls.Clear();

            int correctCount = 0;
            foreach (var a in userAnswers)
            {
                if (a.IsCorrect) correctCount++;
            }
            int total = currentQuestions.Count;
            int percentage = (int)Math.Round(((double)correctCount / total) * 100);

            Panel resultCard = new Panel();
            resultCard.Dock = DockStyle.Fill;
            resultCard.BackColor = ColorCard;
            resultCard.Padding = new Padding(24);

            string tagText;
            Color tagColor;
            string feedbackMsg;

            if (correctCount == 5)
            {
                tagText = "🏆 PERFEKCYJNIE!";
                tagColor = ColorSuccess;
                feedbackMsg = "Gratulacje! Odpowiedziałeś poprawnie na wszystkie pytania!";
            }
            else if (correctCount >= 4)
            {
                tagText = "🌟 BARDZO DOBRZE!";
                tagColor = Color.FromArgb(56, 189, 248);
                feedbackMsg = "Świetna wiedza szkolna! Prawie maksymalny wynik!";
            }
            else if (correctCount >= 3)
            {
                tagText = "👍 DOBRY WYNIK!";
                tagColor = Color.FromArgb(250, 204, 21);
                feedbackMsg = "Większość odpowiedzi jest poprawna. Dobra robota!";
            }
            else
            {
                tagText = "📚 WARTO POWTÓRZYĆ!";
                tagColor = ColorDanger;
                feedbackMsg = "Nie przejmuj się! Powtórz materiał i spróbuj jeszcze raz.";
            }

            Label tagLbl = new Label();
            tagLbl.Text = tagText;
            tagLbl.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            tagLbl.ForeColor = tagColor;
            tagLbl.TextAlign = ContentAlignment.MiddleCenter;
            tagLbl.Dock = DockStyle.Top;
            tagLbl.Height = 35;
            resultCard.Controls.Add(tagLbl);

            Label scoreLbl = new Label();
            scoreLbl.Text = string.Format("{0} / {1}", correctCount, total);
            scoreLbl.Font = new Font("Segoe UI", 36, FontStyle.Bold);
            scoreLbl.ForeColor = ColorTextMain;
            scoreLbl.TextAlign = ContentAlignment.MiddleCenter;
            scoreLbl.Dock = DockStyle.Top;
            scoreLbl.Height = 65;
            resultCard.Controls.Add(scoreLbl);

            Label pctLbl = new Label();
            pctLbl.Text = string.Format("Poprawne odpowiedzi: {0}%  -  {1}", percentage, feedbackMsg);
            pctLbl.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            pctLbl.ForeColor = ColorTextMuted;
            pctLbl.TextAlign = ContentAlignment.MiddleCenter;
            pctLbl.Dock = DockStyle.Top;
            pctLbl.Height = 30;
            resultCard.Controls.Add(pctLbl);

            Label summaryHeader = new Label();
            summaryHeader.Text = "Podsumowanie wylosowanych pytań i Twoich odpowiedzi:";
            summaryHeader.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            summaryHeader.ForeColor = ColorTextMain;
            summaryHeader.Dock = DockStyle.Top;
            summaryHeader.Height = 35;
            resultCard.Controls.Add(summaryHeader);

            Panel listPanel = new Panel();
            listPanel.Dock = DockStyle.Fill;
            listPanel.AutoScroll = true;

            int curY = 0;
            for (int i = 0; i < userAnswers.Count; i++)
            {
                UserAnswer ua = userAnswers[i];
                Question q = ua.QuestionData;

                Panel row = new Panel();
                row.Location = new Point(0, curY);
                row.Size = new Size(mainContainer.Width - 100, 48);
                row.BackColor = ColorBg;
                row.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                Label iconLbl = new Label();
                iconLbl.Text = ua.IsCorrect ? "✓" : "✗";
                iconLbl.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                iconLbl.ForeColor = ua.IsCorrect ? ColorSuccess : ColorDanger;
                iconLbl.Size = new Size(35, 48);
                iconLbl.TextAlign = ContentAlignment.MiddleCenter;
                row.Controls.Add(iconLbl);

                Label qTitle = new Label();
                qTitle.Text = q.Category + " - " + q.QuestionText;
                qTitle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                qTitle.ForeColor = ColorTextMain;
                qTitle.Location = new Point(40, 5);
                qTitle.Size = new Size(row.Width - 50, 18);
                qTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                row.Controls.Add(qTitle);

                Label ansDetail = new Label();
                if (ua.IsCorrect)
                {
                    ansDetail.Text = "Twoja odpowiedź: " + q.Options[ua.ChosenOption];
                    ansDetail.ForeColor = ColorSuccess;
                }
                else
                {
                    ansDetail.Text = string.Format("Twoja odpowiedź: {0} | Poprawna: {1}", q.Options[ua.ChosenOption], q.Options[ua.CorrectOption]);
                    ansDetail.ForeColor = Color.FromArgb(248, 113, 113);
                }
                ansDetail.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                ansDetail.Location = new Point(40, 25);
                ansDetail.Size = new Size(row.Width - 50, 18);
                ansDetail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                row.Controls.Add(ansDetail);

                listPanel.Controls.Add(row);
                curY += 54;
            }

            resultCard.Controls.Add(listPanel);
            mainContainer.Controls.Add(resultCard);

            Panel footer = new Panel();
            footer.Dock = DockStyle.Bottom;
            footer.Height = 60;
            footer.BackColor = ColorBg;

            Button restartBtn = new Button();
            restartBtn.Text = "🔄 Nowy quiz (losuj inne pytania)";
            restartBtn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            restartBtn.BackColor = ColorAccent;
            restartBtn.ForeColor = Color.White;
            restartBtn.FlatStyle = FlatStyle.Flat;
            restartBtn.FlatAppearance.BorderSize = 0;
            restartBtn.Size = new Size(260, 44);
            restartBtn.Location = new Point(0, 10);
            restartBtn.Cursor = Cursors.Hand;
            restartBtn.Click += (s, e) => RestartQuiz();
            footer.Controls.Add(restartBtn);

            Button exitBtn = new Button();
            exitBtn.Text = "Zamknij aplikację";
            exitBtn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            exitBtn.BackColor = ColorBtnBg;
            exitBtn.ForeColor = ColorTextMuted;
            exitBtn.FlatStyle = FlatStyle.Flat;
            exitBtn.FlatAppearance.BorderSize = 0;
            exitBtn.Size = new Size(160, 44);
            exitBtn.Location = new Point(mainContainer.Width - 210, 10);
            exitBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            exitBtn.Cursor = Cursors.Hand;
            exitBtn.Click += (s, e) => Application.Exit();
            footer.Controls.Add(exitBtn);

            mainContainer.Controls.Add(footer);
        }

        private void RestartQuiz()
        {
            currentIndex = 0;
            userAnswers.Clear();
            selectedOption = -1;
            PickRandomQuestions();
            ShowQuestionScreen();
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new QuizForm());
        }
    }
}
