#include <iostream>

int rec(int number){
    if(number == 0){
        return 10;
    }else{
        int min_child = rec(number/10);
        if(number%10 < min_child){
            min_child = number%10;
        }else{
            return min_child;
        }
    }
}
//на возрате
int rec(int number){
    if(number < 10){
        return number;
    }else{
        int min_child = rec(number/10);
        int digit = number % 10;
        if(min_child > digit){
            min_child = digit;
        }
        return min_child;
    }
}

//на спуске
int rec_2(int number, int min){
    if(number == 0){
        return min;
    }else{
        if(number % 10 < min){
            min  = number % 10;
        }
        rec_2(number/10, min);
    }
}
int main(){
    int number;
    int min_child;

    std::cout << "Введите число : ";
    std::cin >> number;

    min_child = rec(number);

    std::cout << min_child;

    return 0;
}