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
3. Unit Literals: Jeśli jeden z literałów już jest ustawiony na prawdę, to nie rozwarzać pozostałych w tej części (a or b or !c przy założeniu, że a = true)
4. Pure Literals: Jeśli jakiś literał pojawia się w całej formule tylko w swojej negacji, to ustaw go na false
5. Heurystyka: Jak wybrać V? Dla STA: Wybrać ten, co się pojawia jak najczęściaj w jak najkrótrzych składowych. Ja myślę, czy dla naszego 

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
