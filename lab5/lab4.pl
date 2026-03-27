:- initialization(main, main).


stronger(herbert, thomas).
stronger(francis, thomas).
stronger(francis, herbert).
stronger(james, herbert).
stronger(herbert, francis). 


stronger(X, Z) :-
    stronger(X, Y),
    stronger(Y, Z).


weaker(X, Y) :-
    stronger(Y, X).


order([A, B, C, D]) :-
    permutation([thomas, herbert, francis, james], [A, B, C, D]),
    weaker(A, B),
    weaker(B, C),
    weaker(C, D).

main :-
    (   order(Order)
    ->  write('Order: '), writeln(Order)
    ;   writeln('No consistent ordering exists (contradiction in data)')
    ),
    halt.