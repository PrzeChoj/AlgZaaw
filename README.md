# Algorytmy Zaawansowane

Adam Przemysław Chojecki

Szymon Tur

# Polecenie - 3SAT
Zaprojektuj i zaimplementuj algorytm sprawdzający spełnialność formuły logicznej w postaci 3-CNF w czasie krótszym niż $O(2^n)$, gdzie n jest liczbą zmiennych.


# DPLL algorithm
Modyfikowany DFS
Dla problemu SAT Worst-case performance $O(2^{n})$
Ale my mamy #SAT, więc może będzie szybciej?

Poniżej opis na podstawie [filmu na YT](https://www.youtube.com/watch?v=xFpndTg7ZqA):
1. Jak ustalamy jedną zmienną, to wszystkie ją zawierające części skracają się
2. Jeśli jakaś formuła ma tylko jeden literał w sobie (np. a or a or a) ale nie zmieniający swojej wartości (czyli a or !a or !a jest częścią niepodpadającą pod tą regułę) to ustawić go na jedyny poprawny
3. Unit Literals: Jeśli jeden z literałów już jest ustawiony na prawdę, to nie rozważać pozostałych w tej części (a or b or !c przy założeniu, że a = true)
4. Pure Literals: Jeśli jakiś literał pojawia się w całej formule tylko w swojej negacji, to ustaw go na false
5. Heurystyka: Jak wybrać V? Dla STA: Wybrać ten, co się pojawia jak najczęściej w jak najkrótszych składowych. Ja myślę, czy dla naszego 

```
dpll(F, literal) {
	usuń te składowe F, co zawierają literał literal
	if (F pusty) return true;
	skróć składowe zawierające !literal
	if (istnieje w F pusta składowa) return false;
	if (F zawiera Unit albo Pure literał L) return dpll(F, L);
	
	wybierz literał V
	return dpll(F, !V) or dpll(F, V);
}
```

# Pomysł
Można znaleźć zbiór literałów, które dzielą klauzule ze wszystkimi innymi literałami. Tworzymy graf $G$, gdzie wierzchołki to literały i dla każdej klauzuli $(a,b,c)$ dodajemy krawędzie $ab,bc,ca$. Wtedy $\delta(G)\geq 2$ i znalazłem w jakimś [paperze](https://arxiv.org/abs/1410.4334), że wtedy istnieje zbiór dominujący o liczności $\gamma(G) \leq \frac{2}{5}n$. 

Bierzemy ten zbiór dominujący ($S$) i zgadujemy wszystkie wartościowania $O(2^{|S|})$. Wtedy wszystkie klauzule zawierające literały z $S$ można zredukować do 2SAT. Pozostają klauzule niezawierające literałów z $S$. Ale skoro $S$ jest dominujący, to w każdy literał w 3-klauzuli jest też obecny w jakiejś $\leq 2$-klauzuli. Mamy więc sytuację (w ogólności, można rozważać prostsze przypadki, które się łatwiej redukuje)

$$
(a \vee b) \wedge (c \vee d) \wedge (e \vee f) \wedge (a \vee c \vee e),
$$

Którą zamieniamy na

$$
(a \wedge (c \vee d) \wedge (e \vee f)) \vee (\neg a \wedge b \wedge (c \vee d) \wedge (e \vee f) \wedge (c \vee e))
$$

i obie części redukujemy dalej osobno (w pierwszej mamy 1 zmienną mniej, w drugiej 2 zmienne mniej). Gdy w 3-klauzuli wszystkie zmienne będą zanegowane, to korzystamy z tego, że

$$
(a \vee b) \wedge (c \vee d) \wedge (e \vee f) \wedge (\neg a \vee \neg c \vee \neg e)\iff(a \vee b) \wedge (c \vee d) \wedge (e \vee f) \wedge (b \vee d \vee f).
$$

Jak nie będzie już więcej 3-klauzul, rozwiązujemy 2SAT.

Złożoność redukcji to $O\left(\left(\frac{1+\sqrt{5}}{2}\right)^{n-|S|}\right)$ (z twierdzenia o rekurencji z ASD2). Całkowita złożoność algorytmu to będzie $O\left(2^{|S|}\cdot\left(\frac{1+\sqrt{5}}{2}\right)^{n-|S|}\right)$, co dla $|S|\leq \frac{2}{5}n$ będzie gdzieś około $O(1,7612^n)$.
