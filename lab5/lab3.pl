:- initialization(main, main).


member(X, [X|_]).
member(X, [_|T]) :-
    member(X, T).


add_unique(X, L, L) :-
    member(X, L), !.
add_unique(X, L, [X|L]).


union([], L, L).
union([H|T], L, Result) :-
    add_unique(H, L, NewL),
    union(T, NewL, Result).

intersection([], _, []).
intersection([H|T], L, [H|Res]) :-
    member(H, L),
    intersection(T, L, Res).
intersection([_|T], L, Res) :-
    intersection(T, L, Res).

subset([], _).
subset([H|T], L) :-
    member(H, L),
    subset(T, L).

equal_sets(A, B) :-
    subset(A, B),
    subset(B, A).


main :-
    write('Enter set A: '),
    read(A),
    write('Enter set B: '),
    read(B),
    write('Enter set C: '),
    read(C),

    union(B, C, BC),
    intersection(A, BC, Left),

    intersection(A, B, AB),
    intersection(A, C, AC),
    union(AB, AC, Right),

    write('Left = '), write(Left), nl,
    write('Right = '), write(Right), nl,

    (   equal_sets(Left, Right)
    ->  writeln('Distributive law holds')
    ;   writeln('Distributive law does NOT hold')
    ),

    halt.