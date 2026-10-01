"""
Szkolny Quiz Wiedzy - Aplikacja Desktopowa (Tkinter)
Autor: Lukasz
Opis: Interaktywny quiz losujący za każdym razem 5 różnych pytań
      z bogatej bazy przedmiotów szkolnych (Geografia, Historia, Biologia, Chemia, Matematyka, Fizyka, Informatyka, Język Polski).
"""

import tkinter as tk
from tkinter import font as tkfont
import random

# Baza pytań ze szkolnych przedmiotów (z tej bazy losowanych jest 5 pytań przy każdym uruchomieniu)
QUESTION_POOL = [
    {
        "category": "🌍 Geografia",
        "question": "Jaka jest oficjalna stolica Australii?",
        "options": ["Sydney", "Melbourne", "Canberra", "Brisbane"],
        "correct": 2,
        "explanation": "Stolicą Australii jest Canberra (nie Sydney ani Melbourne)."
    },
    {
        "category": "🌍 Geografia",
        "question": "Jaka jest najdłuższa rzeka w Polsce?",
        "options": ["Odra", "Wisła", "Warta", "Bug"],
        "correct": 1,
        "explanation": "Wisła ma długość 1047 km i jest najdłuższą rzeką w Polsce."
    },
    {
        "category": "🌍 Geografia",
        "question": "Na jakim kontynencie znajduje się pustynia Sahara?",
        "options": ["Azja", "Ameryka Południowa", "Australia", "Afryka"],
        "correct": 3,
        "explanation": "Sahara znajduje się w północnej części Afryki."
    },
    {
        "category": "⚔️ Historia",
        "question": "W którym roku miała miejsce słynna bitwa pod Grunwaldem?",
        "options": ["1385 r.", "1410 r.", "1466 r.", "1525 r."],
        "correct": 1,
        "explanation": "Bitwa pod Grunwaldem rozegrała się 15 lipca 1410 roku."
    },
    {
        "category": "⚔️ Historia",
        "question": "W którym roku odbył się chrzest Polski?",
        "options": ["966 r.", "1000 r.", "1025 r.", "1138 r."],
        "correct": 0,
        "explanation": "Chrzest Polski pod przywództwem Mieszka I odbył się w 966 roku."
    },
    {
        "category": "⚔️ Historia",
        "question": "Kto był pierwszym koronowanym królem Polski?",
        "options": ["Mieszko I", "Bolesław Chrobry", "Kazimierz Wielki", "Władysław Łokietek"],
        "correct": 1,
        "explanation": "Bolesław Chrobry został koronowany w 1025 roku."
    },
    {
        "category": "🧬 Biologia",
        "question": "Który organ w ciele człowieka odpowiada za produkcję insuliny?",
        "options": ["Wątroba", "Trzustka", "Żołądek", "Nerki"],
        "correct": 1,
        "explanation": "Insulina jest wytwarzana przez komórki beta trzustki."
    },
    {
        "category": "🧬 Biologia",
        "question": "Jak nazywa się podstawowa jednostka budulcowa i funkcjonalna organizmów?",
        "options": ["Tkanka", "Komórka", "Narząd", "Białko"],
        "correct": 1,
        "explanation": "Podstawową jednostką strukturalną i funkcjonalną życia jest komórka."
    },
    {
        "category": "🧬 Biologia",
        "question": "Który barwnik w roślinach odpowiada za proces fotosyntezy?",
        "options": ["Melanina", "Karoten", "Chlorofil", "Hemoglobina"],
        "correct": 2,
        "explanation": "Chlorofil pochłania światło słoneczne niezbędne do fotosyntezy."
    },
    {
        "category": "⚗️ Chemia",
        "question": "Jaki jest symbol chemiczny złota w układzie okresowym pierwiastków?",
        "options": ["Ag", "Fe", "Au", "Pt"],
        "correct": 2,
        "explanation": "Symbol Au pochodzi od łacińskiej nazwy Aurum."
    },
    {
        "category": "⚗️ Chemia",
        "question": "Jaki odczyn ma roztwór o wartości pH równej dokładnie 7?",
        "options": ["Kwasowy", "Zasadowy", "Obojętny", "Żrący"],
        "correct": 2,
        "explanation": "Wartość pH = 7 oznacza odczyn obojętny (np. czysta woda)."
    },
    {
        "category": "⚗️ Chemia",
        "question": "Jaki jest wzór chemiczny wody?",
        "options": ["CO2", "H2O", "NaCl", "O2"],
        "correct": 1,
        "explanation": "Cząsteczka wody składa się z dwóch atomów wodoru i jednego atomu tlenu (H2O)."
    },
    {
        "category": "📐 Matematyka",
        "question": "Ile wynosi wynik działania: 2 + 2 × 2?",
        "options": ["6", "8", "4", "10"],
        "correct": 0,
        "explanation": "Zgodnie z kolejnością działań: najpierw mnożenie 2×2=4, a potem dodawanie 2+4=6."
    },
    {
        "category": "📐 Matematyka",
        "question": "Ile wynosi suma miar kątów wewnętrznych w dowolnym trójkącie?",
        "options": ["90°", "180°", "270°", "360°"],
        "correct": 1,
        "explanation": "W geometrii euklidesowej suma kątów w każdym trójkącie wynosi 180°."
    },
    {
        "category": "📐 Matematyka",
        "question": "Ile wynosi pierwiastek kwadratowy z liczby 144?",
        "options": ["11", "12", "13", "14"],
        "correct": 1,
        "explanation": "12 × 12 = 144, więc pierwiastek ze 144 wynosi 12."
    },
    {
        "category": "⚡ Fizyka",
        "question": "W jakiej jednostce w układzie SI mierzymy siłę?",
        "options": ["Dżul (J)", "Wat (W)", "Niuton (N)", "Paskal (Pa)"],
        "correct": 2,
        "explanation": "Jednostką siły w układzie SI jest Niuton (N)."
    },
    {
        "category": "💻 Informatyka",
        "question": "Ile bitów mieści się w jednym bajcie (1 B)?",
        "options": ["4 bity", "8 bitów", "16 bitów", "32 bity"],
        "correct": 1,
        "explanation": "1 bajt (Byte) składa się dokładnie z 8 bitów."
    },
    {
        "category": "📚 Język Polski",
        "question": "Kto jest autorem polskiej epopei narodowej „Pan Tadeusz”?",
        "options": ["Juliusz Słowacki", "Henryk Sienkiewicz", "Adam Mickiewicz", "Bolesław Prus"],
        "correct": 2,
        "explanation": "„Pana Tadeusza” napisał Adam Mickiewicz w Paryżu w latach 1832–1834."
    }
]

# Kolory nowoczesnego motywu (Dark Slate / Indigo)
THEME = {
    "bg": "#0F172A",
    "card_bg": "#1E293B",
    "card_border": "#334155",
    "text_primary": "#F8FAFC",
    "text_secondary": "#94A3B8",
    "accent": "#6366F1",
    "accent_hover": "#4F46E5",
    "btn_bg": "#334155",
    "btn_hover": "#475569",
    "btn_selected": "#4338CA",
    "btn_selected_border": "#818CF8",
    "success": "#22C55E",
    "danger": "#EF4444",
    "badge_bg": "#312E81",
    "badge_text": "#C7D2FE",
    "progress_bg": "#1E293B",
    "progress_fill": "#6366F1"
}


class QuizApp(tk.Tk):
    def __init__(self):
        super().__init__()

        self.title("Szkolny Quiz Wiedzy - Losowe Pytania")
        self.geometry("760x680")
        self.minsize(700, 620)
        self.configure(bg=THEME["bg"])
        self.center_window(760, 680)

        # Definicje czcionek
        self.font_title = tkfont.Font(family="Segoe UI", size=18, weight="bold")
        self.font_badge = tkfont.Font(family="Segoe UI", size=10, weight="bold")
        self.font_question = tkfont.Font(family="Segoe UI", size=14, weight="bold")
        self.font_option = tkfont.Font(family="Segoe UI", size=11, weight="normal")
        self.font_btn = tkfont.Font(family="Segoe UI", size=12, weight="bold")
        self.font_regular = tkfont.Font(family="Segoe UI", size=10)
        self.font_score_big = tkfont.Font(family="Segoe UI", size=32, weight="bold")

        # Stan quizu
        self.current_index = 0
        self.selected_option = tk.IntVar(value=-1)
        self.current_questions = []
        self.user_answers = []

        # Losowanie 5 pytań na start
        self.pick_random_questions()

        self.main_container = tk.Frame(self, bg=THEME["bg"])
        self.main_container.pack(fill=tk.BOTH, expand=True, padx=25, pady=25)

        self.show_question_screen()

    def pick_random_questions(self):
        """Losuje 5 unikalnych pytań z całej bazy pytań"""
        count = min(5, len(QUESTION_POOL))
        self.current_questions = random.sample(QUESTION_POOL, count)

    def center_window(self, width, height):
        self.update_idletasks()
        screen_w = self.winfo_screenwidth()
        screen_h = self.winfo_screenheight()
        x = max(0, (screen_w // 2) - (width // 2))
        y = max(0, (screen_h // 2) - (height // 2) - 30)
        self.geometry(f"{width}x{height}+{x}+{y}")

    def clear_container(self):
        for widget in self.main_container.winfo_children():
            widget.destroy()

    def show_question_screen(self):
        self.clear_container()
        self.selected_option.set(-1)

        q_data = self.current_questions[self.current_index]
        total_questions = len(self.current_questions)

        # Nagłówek górny
        header_frame = tk.Frame(self.main_container, bg=THEME["bg"])
        header_frame.pack(fill=tk.X, pady=(0, 15))

        top_info = tk.Frame(header_frame, bg=THEME["bg"])
        top_info.pack(fill=tk.X)

        title_lbl = tk.Label(
            top_info,
            text="🎓 SZKOLNY QUIZ WIEDZY",
            font=self.font_title,
            fg=THEME["text_primary"],
            bg=THEME["bg"]
        )
        title_lbl.pack(side=tk.LEFT)

        progress_lbl = tk.Label(
            top_info,
            text=f"Pytanie {self.current_index + 1} z {total_questions}",
            font=self.font_btn,
            fg=THEME["accent"],
            bg=THEME["bg"]
        )
        progress_lbl.pack(side=tk.RIGHT)

        progress_canvas = tk.Canvas(header_frame, height=8, bg="#1E293B", highlightthickness=0)
        progress_canvas.pack(fill=tk.X, pady=(10, 0))
        self.draw_progress_bar(progress_canvas, (self.current_index + 1) / total_questions)

        # Karta pytania
        card = tk.Frame(
            self.main_container,
            bg=THEME["card_bg"],
            highlightbackground=THEME["card_border"],
            highlightthickness=1,
            padx=25,
            pady=20
        )
        card.pack(fill=tk.BOTH, expand=True)

        badge_frame = tk.Frame(card, bg=THEME["badge_bg"], padx=10, pady=4)
        badge_frame.pack(anchor="w", pady=(0, 12))

        cat_lbl = tk.Label(
            badge_frame,
            text=q_data["category"],
            font=self.font_badge,
            fg=THEME["badge_text"],
            bg=THEME["badge_bg"]
        )
        cat_lbl.pack()

        q_lbl = tk.Label(
            card,
            text=q_data["question"],
            font=self.font_question,
            fg=THEME["text_primary"],
            bg=THEME["card_bg"],
            wraplength=640,
            justify="left",
            anchor="w"
        )
        q_lbl.pack(fill=tk.X, pady=(0, 20))

        self.options_frame = tk.Frame(card, bg=THEME["card_bg"])
        self.options_frame.pack(fill=tk.BOTH, expand=True)

        letters = ["A", "B", "C", "D"]
        self.option_widgets = []

        for idx, option_text in enumerate(q_data["options"]):
            btn_box = tk.Frame(
                self.options_frame,
                bg=THEME["btn_bg"],
                highlightbackground=THEME["card_border"],
                highlightthickness=1,
                cursor="hand2"
            )
            btn_box.pack(fill=tk.X, pady=6)

            letter_lbl = tk.Label(
                btn_box,
                text=letters[idx],
                font=self.font_badge,
                bg="#475569",
                fg=THEME["text_primary"],
                width=3,
                pady=10
            )
            letter_lbl.pack(side=tk.LEFT, fill=tk.Y, padx=(1, 10))

            txt_lbl = tk.Label(
                btn_box,
                text=option_text,
                font=self.font_option,
                fg=THEME["text_primary"],
                bg=THEME["btn_bg"],
                anchor="w",
                justify="left"
            )
            txt_lbl.pack(side=tk.LEFT, fill=tk.BOTH, expand=True, pady=10)

            for w in (btn_box, letter_lbl, txt_lbl):
                w.bind("<Button-1>", lambda e, opt_idx=idx: self.select_option(opt_idx))
                w.bind("<Enter>", lambda e, b=btn_box, i=idx: self.on_hover_enter(b, i))
                w.bind("<Leave>", lambda e, b=btn_box, i=idx: self.on_hover_leave(b, i))

            self.option_widgets.append({
                "frame": btn_box,
                "letter": letter_lbl,
                "text": txt_lbl,
                "index": idx
            })

        self.error_lbl = tk.Label(
            card,
            text="",
            font=self.font_regular,
            fg=THEME["danger"],
            bg=THEME["card_bg"]
        )
        self.error_lbl.pack(anchor="w", pady=(8, 0))

        # Stopka
        footer_frame = tk.Frame(self.main_container, bg=THEME["bg"])
        footer_frame.pack(fill=tk.X, pady=(15, 0))

        btn_text = "Następne pytanie ➔" if self.current_index < total_questions - 1 else "Zobacz wynik 🏁"
        self.next_btn = tk.Button(
            footer_frame,
            text=btn_text,
            font=self.font_btn,
            bg=THEME["accent"],
            fg="#FFFFFF",
            activebackground=THEME["accent_hover"],
            activeforeground="#FFFFFF",
            relief=tk.FLAT,
            padx=25,
            pady=10,
            cursor="hand2",
            command=self.submit_answer
        )
        self.next_btn.pack(side=tk.RIGHT)

    def draw_progress_bar(self, canvas, ratio):
        self.update_idletasks()
        width = self.main_container.winfo_width()
        if width <= 1:
            width = 710
        fill_width = max(10, int(width * ratio))
        canvas.delete("all")
        canvas.create_rectangle(0, 0, width, 8, fill="#1E293B", width=0)
        canvas.create_rectangle(0, 0, fill_width, 8, fill=THEME["accent"], width=0)

    def on_hover_enter(self, frame, idx):
        if self.selected_option.get() != idx:
            frame.configure(bg=THEME["btn_hover"])
            for child in frame.winfo_children():
                if child != frame.winfo_children()[0]:
                    child.configure(bg=THEME["btn_hover"])

    def on_hover_leave(self, frame, idx):
        if self.selected_option.get() != idx:
            frame.configure(bg=THEME["btn_bg"])
            for child in frame.winfo_children():
                if child != frame.winfo_children()[0]:
                    child.configure(bg=THEME["btn_bg"])

    def select_option(self, idx):
        self.selected_option.set(idx)
        self.error_lbl.configure(text="")

        for item in self.option_widgets:
            f = item["frame"]
            txt = item["text"]
            let = item["letter"]
            if item["index"] == idx:
                f.configure(
                    bg=THEME["btn_selected"],
                    highlightbackground=THEME["btn_selected_border"],
                    highlightthickness=2
                )
                txt.configure(bg=THEME["btn_selected"])
                let.configure(bg=THEME["accent"])
            else:
                f.configure(
                    bg=THEME["btn_bg"],
                    highlightbackground=THEME["card_border"],
                    highlightthickness=1
                )
                txt.configure(bg=THEME["btn_bg"])
                let.configure(bg="#475569")

    def submit_answer(self):
        sel = self.selected_option.get()
        if sel == -1:
            self.error_lbl.configure(text="⚠️ Proszę wybrać jedną z odpowiedzi przed przejściem dalej!")
            return

        current_q = self.current_questions[self.current_index]
        correct_idx = current_q["correct"]
        self.user_answers.append({
            "question_data": current_q,
            "chosen": sel,
            "correct": correct_idx,
            "is_correct": (sel == correct_idx)
        })

        self.current_index += 1
        if self.current_index < len(self.current_questions):
            self.show_question_screen()
        else:
            self.show_results_screen()

    def show_results_screen(self):
        self.clear_container()

        correct_count = sum(1 for a in self.user_answers if a["is_correct"])
        total_questions = len(self.current_questions)
        percentage = int((correct_count / total_questions) * 100)

        if correct_count == 5:
            feedback_badge = "🏆 PERFEKCYJNIE!"
            feedback_msg = "Gratulacje! Odpowiedziałeś poprawnie na wszystkie pytania!"
            accent_color = THEME["success"]
        elif correct_count >= 4:
            feedback_badge = "🌟 BARDZO DOBRZE!"
            feedback_msg = "Świetna wiedza! Prawie idealny wynik!"
            accent_color = "#38BDF8"
        elif correct_count >= 3:
            feedback_badge = "👍 DOBRY WYNIK!"
            feedback_msg = "Większość odpowiedzi jest poprawna. Dobra robota!"
            accent_color = "#FACC15"
        else:
            feedback_badge = "📚 WARTO POWTÓRZYĆ!"
            feedback_msg = "Nie przejmuj się! Powtórz materiał i spróbuj jeszcze raz."
            accent_color = THEME["danger"]

        card = tk.Frame(
            self.main_container,
            bg=THEME["card_bg"],
            highlightbackground=THEME["card_border"],
            highlightthickness=1,
            padx=25,
            pady=20
        )
        card.pack(fill=tk.BOTH, expand=True)

        top_bar = tk.Frame(card, bg=THEME["card_bg"])
        top_bar.pack(fill=tk.X, pady=(0, 10))

        result_tag = tk.Label(
            top_bar,
            text=feedback_badge,
            font=self.font_btn,
            fg=accent_color,
            bg=THEME["card_bg"]
        )
        result_tag.pack(anchor="center")

        score_frame = tk.Frame(card, bg=THEME["card_bg"])
        score_frame.pack(pady=5)

        score_lbl = tk.Label(
            score_frame,
            text=f"{correct_count} / {total_questions}",
            font=self.font_score_big,
            fg=THEME["text_primary"],
            bg=THEME["card_bg"]
        )
        score_lbl.pack()

        score_sub = tk.Label(
            score_frame,
            text=f"Poprawne odpowiedzi: {percentage}%",
            font=self.font_badge,
            fg=THEME["text_secondary"],
            bg=THEME["card_bg"]
        )
        score_sub.pack()

        feedback_lbl = tk.Label(
            card,
            text=feedback_msg,
            font=self.font_regular,
            fg=THEME["text_secondary"],
            bg=THEME["card_bg"]
        )
        feedback_lbl.pack(pady=(0, 15))

        breakdown_title = tk.Label(
            card,
            text="Podsumowanie wylosowanych pytań i Twoich odpowiedzi:",
            font=self.font_btn,
            fg=THEME["text_primary"],
            bg=THEME["card_bg"]
        )
        breakdown_title.pack(anchor="w", pady=(5, 8))

        breakdown_container = tk.Frame(card, bg=THEME["card_bg"])
        breakdown_container.pack(fill=tk.BOTH, expand=True)

        for item in self.user_answers:
            q_info = item["question_data"]
            is_ok = item["is_correct"]

            row = tk.Frame(breakdown_container, bg="#0F172A", padx=12, pady=6)
            row.pack(fill=tk.X, pady=3)

            icon = "✓" if is_ok else "✗"
            icon_color = THEME["success"] if is_ok else THEME["danger"]

            status_lbl = tk.Label(
                row,
                text=icon,
                font=self.font_btn,
                fg=icon_color,
                bg="#0F172A",
                width=2
            )
            status_lbl.pack(side=tk.LEFT)

            details_box = tk.Frame(row, bg="#0F172A")
            details_box.pack(side=tk.LEFT, fill=tk.X, expand=True, padx=8)

            q_text_short = q_info["question"]
            if len(q_text_short) > 65:
                q_text_short = q_text_short[:62] + "..."

            q_row_lbl = tk.Label(
                details_box,
                text=f"{q_info['category']} - {q_text_short}",
                font=self.font_badge,
                fg=THEME["text_primary"],
                bg="#0F172A",
                anchor="w"
            )
            q_row_lbl.pack(fill=tk.X)

            chosen_txt = q_info["options"][item["chosen"]]
            correct_txt = q_info["options"][item["correct"]]

            if is_ok:
                ans_summary = f"Twoja odpowiedź: {chosen_txt}"
                ans_color = THEME["success"]
            else:
                ans_summary = f"Twoja odpowiedź: {chosen_txt} | Poprawna: {correct_txt}"
                ans_color = "#F87171"

            ans_lbl = tk.Label(
                details_box,
                text=ans_summary,
                font=self.font_regular,
                fg=ans_color,
                bg="#0F172A",
                anchor="w"
            )
            ans_lbl.pack(fill=tk.X)

        footer = tk.Frame(self.main_container, bg=THEME["bg"])
        footer.pack(fill=tk.X, pady=(15, 0))

        restart_btn = tk.Button(
            footer,
            text="🔄 Nowy quiz (losuj inne pytania)",
            font=self.font_btn,
            bg=THEME["accent"],
            fg="#FFFFFF",
            activebackground=THEME["accent_hover"],
            activeforeground="#FFFFFF",
            relief=tk.FLAT,
            padx=20,
            pady=10,
            cursor="hand2",
            command=self.restart_quiz
        )
        restart_btn.pack(side=tk.LEFT)

        exit_btn = tk.Button(
            footer,
            text="Zamknij aplikację",
            font=self.font_btn,
            bg=THEME["btn_bg"],
            fg=THEME["text_secondary"],
            activebackground=THEME["btn_hover"],
            activeforeground="#FFFFFF",
            relief=tk.FLAT,
            padx=20,
            pady=10,
            cursor="hand2",
            command=self.destroy
        )
        exit_btn.pack(side=tk.RIGHT)

    def restart_quiz(self):
        """Resetuje quiz i losuje nową pulę 5 pytań"""
        self.current_index = 0
        self.user_answers.clear()
        self.selected_option.set(-1)
        self.pick_random_questions()
        self.show_question_screen()


if __name__ == "__main__":
    app = QuizApp()
    app.mainloop()
