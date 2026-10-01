import javax.swing.*;
import javax.swing.border.EmptyBorder;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class QuizApp extends JFrame {

    static class Question {
        String category;
        String questionText;
        String[] options;
        int correctIndex;

        public Question(String category, String questionText, String[] options, int correctIndex) {
            this.category = category;
            this.questionText = questionText;
            this.options = options;
            this.correctIndex = correctIndex;
        }
    }

    static class UserAnswer {
        Question questionData;
        int chosenOption;
        int correctOption;
        boolean isCorrect;

        public UserAnswer(Question q, int chosenOption, int correctOption) {
            this.questionData = q;
            this.chosenOption = chosenOption;
            this.correctOption = correctOption;
            this.isCorrect = (chosenOption == correctOption);
        }
    }

    private static final Color COLOR_BG = new Color(15, 23, 42);
    private static final Color COLOR_CARD = new Color(30, 41, 59);
    private static final Color COLOR_CARD_BORDER = new Color(51, 65, 85);
    private static final Color COLOR_TEXT_MAIN = new Color(248, 250, 252);
    private static final Color COLOR_TEXT_MUTED = new Color(148, 163, 184);
    private static final Color COLOR_ACCENT = new Color(99, 102, 241);
    private static final Color COLOR_BTN_BG = new Color(51, 65, 85);
    private static final Color COLOR_BTN_HOVER = new Color(71, 85, 105);
    private static final Color COLOR_BTN_SELECTED = new Color(67, 56, 202);
    private static final Color COLOR_BTN_SELECTED_BORDER = new Color(129, 140, 248);
    private static final Color COLOR_SUCCESS = new Color(34, 197, 94);
    private static final Color COLOR_DANGER = new Color(239, 68, 68);
    private static final Color COLOR_BADGE_BG = new Color(49, 46, 129);
    private static final Color COLOR_BADGE_TEXT = new Color(199, 210, 254);

    private final List<Question> questionPool = new ArrayList<>();
    private final List<Question> currentQuestions = new ArrayList<>();
    private final List<UserAnswer> userAnswers = new ArrayList<>();

    private int currentIndex = 0;
    private int selectedOption = -1;

    private JPanel mainPanel;
    private JLabel progressCountLabel;
    private JProgressBar progressBar;
    private JLabel badgeLabel;
    private JLabel questionLabel;
    private JPanel optionsPanel;
    private List<JPanel> optionCards;
    private List<JLabel> optionTextLabels;
    private List<JLabel> optionBadgeLabels;
    private JLabel errorLabel;
    private JButton nextButton;

    public QuizApp() {
        initPool();
        pickRandomQuestions();
        initWindow();
        showQuestionView();
    }

    private void initPool() {
        questionPool.add(new Question("🌍 Geografia", "Jaka jest oficjalna stolica Australii?", new String[]{"Sydney", "Melbourne", "Canberra", "Brisbane"}, 2));
        questionPool.add(new Question("🌍 Geografia", "Jaka jest najdłuższa rzeka w Polsce?", new String[]{"Odra", "Wisła", "Warta", "Bug"}, 1));
        questionPool.add(new Question("🌍 Geografia", "Na jakim kontynencie znajduje się pustynia Sahara?", new String[]{"Azja", "Ameryka Południowa", "Australia", "Afryka"}, 3));
        questionPool.add(new Question("⚔️ Historia", "W którym roku miała miejsce słynna bitwa pod Grunwaldem?", new String[]{"1385 r.", "1410 r.", "1466 r.", "1525 r."}, 1));
        questionPool.add(new Question("⚔️ Historia", "W którym roku odbył się chrzest Polski?", new String[]{"966 r.", "1000 r.", "1025 r.", "1138 r."}, 0));
        questionPool.add(new Question("⚔️ Historia", "Kto był pierwszym koronowanym królem Polski?", new String[]{"Mieszko I", "Bolesław Chrobry", "Kazimierz Wielki", "Władysław Łokietek"}, 1));
        questionPool.add(new Question("🧬 Biologia", "Który organ w ciele człowieka odpowiada za produkcję insuliny?", new String[]{"Wątroba", "Trzustka", "Żołądek", "Nerki"}, 1));
        questionPool.add(new Question("🧬 Biologia", "Jak nazywa się podstawowa jednostka strukturalna organizmów żywych?", new String[]{"Tkanka", "Komórka", "Narząd", "Białko"}, 1));
        questionPool.add(new Question("🧬 Biologia", "Który barwnik w roślinach odpowiada za fotosyntezę?", new String[]{"Melanina", "Karoten", "Chlorofil", "Hemoglobina"}, 2));
        questionPool.add(new Question("⚗️ Chemia", "Jaki jest symbol chemiczny złota w układzie okresowym pierwiastków?", new String[]{"Ag", "Fe", "Au", "Pt"}, 2));
        questionPool.add(new Question("⚗️ Chemia", "Jaki odczyn ma roztwór o pH równym dokładnie 7?", new String[]{"Kwasowy", "Zasadowy", "Obojętny", "Żrący"}, 2));
        questionPool.add(new Question("⚗️ Chemia", "Jaki jest wzór chemiczny wody?", new String[]{"CO2", "H2O", "NaCl", "O2"}, 1));
        questionPool.add(new Question("📐 Matematyka", "Ile wynosi wynik działania: 2 + 2 × 2?", new String[]{"6", "8", "4", "10"}, 0));
        questionPool.add(new Question("📐 Matematyka", "Ile wynosi suma kątów w dowolnym trójkącie?", new String[]{"90°", "180°", "270°", "360°"}, 1));
        questionPool.add(new Question("📐 Matematyka", "Ile wynosi pierwiastek kwadratowy z liczby 144?", new String[]{"11", "12", "13", "14"}, 1));
        questionPool.add(new Question("⚡ Fizyka", "W jakiej jednostce w układzie SI mierzymy siłę?", new String[]{"Dżul (J)", "Wat (W)", "Niuton (N)", "Paskal (Pa)"}, 2));
        questionPool.add(new Question("💻 Informatyka", "Ile bitów mieści się w jednym bajcie (1 B)?", new String[]{"4 bity", "8 bitów", "16 bitów", "32 bity"}, 1));
        questionPool.add(new Question("📚 Język Polski", "Kto napisał epopeję narodową „Pan Tadeusz”?", new String[]{"Juliusz Słowacki", "Henryk Sienkiewicz", "Adam Mickiewicz", "Bolesław Prus"}, 2));
    }

    private void pickRandomQuestions() {
        List<Question> copy = new ArrayList<>(questionPool);
        Collections.shuffle(copy);
        currentQuestions.clear();
        int count = Math.min(5, copy.size());
        for (int i = 0; i < count; i++) {
            currentQuestions.add(copy.get(i));
        }
    }

    private void initWindow() {
        setTitle("Szkolny Quiz Wiedzy - Losowe Pytania Java Swing");
        setSize(780, 720);
        setMinimumSize(new Dimension(720, 640));
        setLocationRelativeTo(null);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        getContentPane().setBackground(COLOR_BG);

        mainPanel = new JPanel();
        mainPanel.setLayout(new BorderLayout(0, 15));
        mainPanel.setBackground(COLOR_BG);
        mainPanel.setBorder(new EmptyBorder(25, 25, 25, 25));

        setContentPane(mainPanel);
    }

    private void showQuestionView() {
        mainPanel.removeAll();
        selectedOption = -1;

        Question currentQ = currentQuestions.get(currentIndex);
        int total = currentQuestions.size();

        JPanel headerPanel = new JPanel();
        headerPanel.setLayout(new BoxLayout(headerPanel, BoxLayout.Y_AXIS));
        headerPanel.setBackground(COLOR_BG);

        JPanel titleRow = new JPanel(new BorderLayout());
        titleRow.setBackground(COLOR_BG);

        JLabel titleLabel = new JLabel("🎓 SZKOLNY QUIZ WIEDZY");
        titleLabel.setFont(new Font("Segoe UI", Font.BOLD, 20));
        titleLabel.setForeground(COLOR_TEXT_MAIN);
        titleRow.add(titleLabel, BorderLayout.WEST);

        progressCountLabel = new JLabel("Pytanie " + (currentIndex + 1) + " z " + total);
        progressCountLabel.setFont(new Font("Segoe UI", Font.BOLD, 15));
        progressCountLabel.setForeground(COLOR_ACCENT);
        titleRow.add(progressCountLabel, BorderLayout.EAST);

        headerPanel.add(titleRow);
        headerPanel.add(Box.createRigidArea(new Dimension(0, 10)));

        progressBar = new JProgressBar(0, total);
        progressBar.setValue(currentIndex + 1);
        progressBar.setPreferredSize(new Dimension(100, 8));
        progressBar.setBackground(COLOR_CARD);
        progressBar.setForeground(COLOR_ACCENT);
        progressBar.setBorderPainted(false);
        headerPanel.add(progressBar);

        mainPanel.add(headerPanel, BorderLayout.NORTH);

        JPanel centerCard = new JPanel();
        centerCard.setLayout(new BoxLayout(centerCard, BoxLayout.Y_AXIS));
        centerCard.setBackground(COLOR_CARD);
        centerCard.setBorder(BorderFactory.createCompoundBorder(
            new LineBorder(COLOR_CARD_BORDER, 1, true),
            new EmptyBorder(24, 24, 24, 24)
        ));

        JPanel badgeContainer = new JPanel(new FlowLayout(FlowLayout.LEFT, 0, 0));
        badgeContainer.setBackground(COLOR_CARD);
        badgeLabel = new JLabel("  " + currentQ.category + "  ");
        badgeLabel.setOpaque(true);
        badgeLabel.setBackground(COLOR_BADGE_BG);
        badgeLabel.setForeground(COLOR_BADGE_TEXT);
        badgeLabel.setFont(new Font("Segoe UI", Font.BOLD, 12));
        badgeLabel.setBorder(new EmptyBorder(4, 8, 4, 8));
        badgeContainer.add(badgeLabel);
        centerCard.add(badgeContainer);
        centerCard.add(Box.createRigidArea(new Dimension(0, 14)));

        questionLabel = new JLabel("<html><body style='width: 580px;'>" + currentQ.questionText + "</body></html>");
        questionLabel.setFont(new Font("Segoe UI", Font.BOLD, 17));
        questionLabel.setForeground(COLOR_TEXT_MAIN);
        centerCard.add(questionLabel);
        centerCard.add(Box.createRigidArea(new Dimension(0, 20)));

        optionsPanel = new JPanel();
        optionsPanel.setLayout(new GridLayout(4, 1, 0, 10));
        optionsPanel.setBackground(COLOR_CARD);

        optionCards = new ArrayList<>();
        optionTextLabels = new ArrayList<>();
        optionBadgeLabels = new ArrayList<>();
        String[] letters = {"A", "B", "C", "D"};

        for (int i = 0; i < currentQ.options.length; i++) {
            final int optIndex = i;
            JPanel optCard = new JPanel(new BorderLayout(15, 0));
            optCard.setBackground(COLOR_BTN_BG);
            optCard.setBorder(BorderFactory.createCompoundBorder(
                new LineBorder(COLOR_CARD_BORDER, 1, true),
                new EmptyBorder(10, 14, 10, 14)
            ));
            optCard.setCursor(new Cursor(Cursor.HAND_CURSOR));

            JLabel letterBadge = new JLabel(letters[i], SwingConstants.CENTER);
            letterBadge.setOpaque(true);
            letterBadge.setBackground(new Color(71, 85, 105));
            letterBadge.setForeground(COLOR_TEXT_MAIN);
            letterBadge.setFont(new Font("Segoe UI", Font.BOLD, 13));
            letterBadge.setPreferredSize(new Dimension(32, 32));
            letterBadge.setBorder(new EmptyBorder(2, 2, 2, 2));

            JLabel optText = new JLabel(currentQ.options[i]);
            optText.setFont(new Font("Segoe UI", Font.PLAIN, 15));
            optText.setForeground(COLOR_TEXT_MAIN);

            optCard.add(letterBadge, BorderLayout.WEST);
            optCard.add(optText, BorderLayout.CENTER);

            optCard.addMouseListener(new MouseAdapter() {
                @Override
                public void mouseClicked(MouseEvent e) {
                    selectOption(optIndex);
                }

                @Override
                public void mouseEntered(MouseEvent e) {
                    if (selectedOption != optIndex) {
                        optCard.setBackground(COLOR_BTN_HOVER);
                    }
                }

                @Override
                public void mouseExited(MouseEvent e) {
                    if (selectedOption != optIndex) {
                        optCard.setBackground(COLOR_BTN_BG);
                    }
                }
            });

            optionCards.add(optCard);
            optionBadgeLabels.add(letterBadge);
            optionTextLabels.add(optText);
            optionsPanel.add(optCard);
        }

        centerCard.add(optionsPanel);
        centerCard.add(Box.createRigidArea(new Dimension(0, 12)));

        errorLabel = new JLabel(" ");
        errorLabel.setFont(new Font("Segoe UI", Font.BOLD, 13));
        errorLabel.setForeground(COLOR_DANGER);
        centerCard.add(errorLabel);

        mainPanel.add(centerCard, BorderLayout.CENTER);

        JPanel footerPanel = new JPanel(new FlowLayout(FlowLayout.RIGHT, 0, 0));
        footerPanel.setBackground(COLOR_BG);

        String btnText = (currentIndex < total - 1) ? "Następne pytanie ➔" : "Zobacz wynik 🏁";
        nextButton = new JButton(btnText);
        nextButton.setFont(new Font("Segoe UI", Font.BOLD, 14));
        nextButton.setBackground(COLOR_ACCENT);
        nextButton.setForeground(Color.WHITE);
        nextButton.setFocusPainted(false);
        nextButton.setBorder(new EmptyBorder(12, 26, 12, 26));
        nextButton.setCursor(new Cursor(Cursor.HAND_CURSOR));
        nextButton.addActionListener(e -> handleSubmit());

        footerPanel.add(nextButton);
        mainPanel.add(footerPanel, BorderLayout.SOUTH);

        mainPanel.revalidate();
        mainPanel.repaint();
    }

    private void selectOption(int index) {
        selectedOption = index;
        errorLabel.setText(" ");

        for (int i = 0; i < optionCards.size(); i++) {
            JPanel card = optionCards.get(i);
            JLabel badge = optionBadgeLabels.get(i);

            if (i == index) {
                card.setBackground(COLOR_BTN_SELECTED);
                card.setBorder(BorderFactory.createCompoundBorder(
                    new LineBorder(COLOR_BTN_SELECTED_BORDER, 2, true),
                    new EmptyBorder(9, 13, 9, 13)
                ));
                badge.setBackground(COLOR_ACCENT);
            } else {
                card.setBackground(COLOR_BTN_BG);
                card.setBorder(BorderFactory.createCompoundBorder(
                    new LineBorder(COLOR_CARD_BORDER, 1, true),
                    new EmptyBorder(10, 14, 10, 14)
                ));
                badge.setBackground(new Color(71, 85, 105));
            }
        }
    }

    private void handleSubmit() {
        if (selectedOption == -1) {
            errorLabel.setText("⚠️ Proszę wybrać jedną z odpowiedzi przed przejściem dalej!");
            return;
        }

        Question q = currentQuestions.get(currentIndex);
        userAnswers.add(new UserAnswer(q, selectedOption, q.correctIndex));

        currentIndex++;
        if (currentIndex < currentQuestions.size()) {
            showQuestionView();
        } else {
            showResultsView();
        }
    }

    private void showResultsView() {
        mainPanel.removeAll();

        int correctCount = 0;
        for (UserAnswer ans : userAnswers) {
            if (ans.isCorrect) correctCount++;
        }
        int total = currentQuestions.size();
        int percentage = (int) Math.round(((double) correctCount / total) * 100);

        JPanel resultCard = new JPanel();
        resultCard.setLayout(new BoxLayout(resultCard, BoxLayout.Y_AXIS));
        resultCard.setBackground(COLOR_CARD);
        resultCard.setBorder(BorderFactory.createCompoundBorder(
            new LineBorder(COLOR_CARD_BORDER, 1, true),
            new EmptyBorder(25, 25, 25, 25)
        ));

        String tagText;
        Color tagColor;
        String descText;

        if (correctCount == 5) {
            tagText = "🏆 PERFEKCYJNIE!";
            tagColor = COLOR_SUCCESS;
            descText = "Gratulacje! Odpowiedziałeś poprawnie na wszystkie pytania!";
        } else if (correctCount >= 4) {
            tagText = "🌟 BARDZO DOBRZE!";
            tagColor = new Color(56, 189, 248);
            descText = "Świetna wiedza szkolna! Prawie maksymalna liczba punktów!";
        } else if (correctCount >= 3) {
            tagText = "👍 DOBRY WYNIK!";
            tagColor = new Color(250, 204, 21);
            descText = "Większość odpowiedzi jest poprawna. Dobra robota!";
        } else {
            tagText = "📚 WARTO POWTÓRZYĆ!";
            tagColor = COLOR_DANGER;
            descText = "Nie zrażaj się! Powtórz materiał i spróbuj jeszcze raz.";
        }

        JLabel tagLabel = new JLabel(tagText);
        tagLabel.setFont(new Font("Segoe UI", Font.BOLD, 18));
        tagLabel.setForeground(tagColor);
        tagLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
        resultCard.add(tagLabel);
        resultCard.add(Box.createRigidArea(new Dimension(0, 10)));

        JLabel scoreLabel = new JLabel(correctCount + " / " + total);
        scoreLabel.setFont(new Font("Segoe UI", Font.BOLD, 48));
        scoreLabel.setForeground(COLOR_TEXT_MAIN);
        scoreLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
        resultCard.add(scoreLabel);

        JLabel percentLabel = new JLabel("Poprawne odpowiedzi: " + percentage + "%");
        percentLabel.setFont(new Font("Segoe UI", Font.BOLD, 15));
        percentLabel.setForeground(COLOR_TEXT_MUTED);
        percentLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
        resultCard.add(percentLabel);
        resultCard.add(Box.createRigidArea(new Dimension(0, 6)));

        JLabel descLabel = new JLabel(descText);
        descLabel.setFont(new Font("Segoe UI", Font.PLAIN, 14));
        descLabel.setForeground(COLOR_TEXT_MUTED);
        descLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
        resultCard.add(descLabel);
        resultCard.add(Box.createRigidArea(new Dimension(0, 20)));

        JLabel breakdownTitle = new JLabel("Podsumowanie wylosowanych pytań i Twoich odpowiedzi:");
        breakdownTitle.setFont(new Font("Segoe UI", Font.BOLD, 15));
        breakdownTitle.setForeground(COLOR_TEXT_MAIN);
        breakdownTitle.setAlignmentX(Component.LEFT_ALIGNMENT);
        resultCard.add(breakdownTitle);
        resultCard.add(Box.createRigidArea(new Dimension(0, 10)));

        JPanel listPanel = new JPanel();
        listPanel.setLayout(new GridLayout(total, 1, 0, 8));
        listPanel.setBackground(COLOR_CARD);

        for (UserAnswer ua : userAnswers) {
            Question q = ua.questionData;
            JPanel row = new JPanel(new BorderLayout(12, 0));
            row.setBackground(new Color(15, 23, 42));
            row.setBorder(new EmptyBorder(8, 12, 8, 12));

            JLabel iconLabel = new JLabel(ua.isCorrect ? "✓" : "✗", SwingConstants.CENTER);
            iconLabel.setFont(new Font("Segoe UI", Font.BOLD, 18));
            iconLabel.setForeground(ua.isCorrect ? COLOR_SUCCESS : COLOR_DANGER);
            iconLabel.setPreferredSize(new Dimension(28, 28));

            JPanel infoBox = new JPanel(new GridLayout(2, 1, 0, 2));
            infoBox.setBackground(new Color(15, 23, 42));

            JLabel qTitle = new JLabel(q.category + " - " + q.questionText);
            qTitle.setFont(new Font("Segoe UI", Font.BOLD, 12));
            qTitle.setForeground(COLOR_TEXT_MAIN);

            String answerDetail;
            if (ua.isCorrect) {
                answerDetail = "Twoja odpowiedź: " + q.options[ua.chosenOption];
            } else {
                answerDetail = "Twoja odpowiedź: " + q.options[ua.chosenOption] + " | Poprawna: " + q.options[ua.correctOption];
            }
            JLabel aDetail = new JLabel(answerDetail);
            aDetail.setFont(new Font("Segoe UI", Font.PLAIN, 12));
            aDetail.setForeground(ua.isCorrect ? COLOR_SUCCESS : new Color(248, 113, 113));

            infoBox.add(qTitle);
            infoBox.add(aDetail);

            row.add(iconLabel, BorderLayout.WEST);
            row.add(infoBox, BorderLayout.CENTER);

            listPanel.add(row);
        }

        resultCard.add(listPanel);
        mainPanel.add(resultCard, BorderLayout.CENTER);

        JPanel footer = new JPanel(new BorderLayout());
        footer.setBackground(COLOR_BG);

        JButton restartBtn = new JButton("🔄 Nowy quiz (losuj inne pytania)");
        restartBtn.setFont(new Font("Segoe UI", Font.BOLD, 14));
        restartBtn.setBackground(COLOR_ACCENT);
        restartBtn.setForeground(Color.WHITE);
        restartBtn.setFocusPainted(false);
        restartBtn.setBorder(new EmptyBorder(12, 22, 12, 22));
        restartBtn.setCursor(new Cursor(Cursor.HAND_CURSOR));
        restartBtn.addActionListener(e -> restartQuiz());

        JButton exitBtn = new JButton("Zamknij aplikację");
        exitBtn.setFont(new Font("Segoe UI", Font.BOLD, 14));
        exitBtn.setBackground(COLOR_BTN_BG);
        exitBtn.setForeground(COLOR_TEXT_MUTED);
        exitBtn.setFocusPainted(false);
        exitBtn.setBorder(new EmptyBorder(12, 22, 12, 22));
        exitBtn.setCursor(new Cursor(Cursor.HAND_CURSOR));
        exitBtn.addActionListener(e -> System.exit(0));

        footer.add(restartBtn, BorderLayout.WEST);
        footer.add(exitBtn, BorderLayout.EAST);

        mainPanel.add(footer, BorderLayout.SOUTH);

        mainPanel.revalidate();
        mainPanel.repaint();
    }

    private void restartQuiz() {
        currentIndex = 0;
        userAnswers.clear();
        selectedOption = -1;
        pickRandomQuestions();
        showQuestionView();
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> {
            QuizApp app = new QuizApp();
            app.setVisible(true);
        });
    }
}
