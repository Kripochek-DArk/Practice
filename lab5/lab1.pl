:- initialization(main, main).

first_digit(N, N) :-
    N < 10.

first_digit(N, D) :-
    N >= 10,
    N1 is N // 10,
    first_digit(N1, D).

last_digit(N, D) :-
    D is N mod 10.

sum_first_last(N, Sum) :-
    first_digit(N, First),
    last_digit(N, Last),
    Sum is First + Last.

main :-
    write('Enter a natural number: '),
    read_line_to_string(user_input, Input),
    number_string(N, Input),
    sum_first_last(N, Sum),
    write('Sum = '),
    write(Sum),
    nl,
    halt.