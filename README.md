# Algorytmy Zaawansowane

Adam Przemysław Chojecki

Szymon Tur

# Polecenie - 3SAT
Zaprojektuj i zaimplementuj algorytm sprawdzający spełnialność formuły logicznej w postaci 3-CNF w czasie krótszym niż $O(2^n)$, gdzie n jest liczbą zmiennych.


# Idea rozwiązania

Skorzystać tylko z tego, że

$$
(a \vee b \vee c) \wedge \Phi \iff (a \wedge \Phi) \vee (\neg a \wedge b \wedge \Phi) \vee (\neg a \wedge \neg b \wedge c \wedge \Phi) 
$$

I wtedy każdą z części rozwiązujemy osobno, wyrzucając odpowiednie zmienne. Czas wykonania algorytmu to

$$
T(n) = T(n-1)+T(n-2)+T(n-3) = O(1,84^n).
$$
