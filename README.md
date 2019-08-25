![](https://i.imgur.com/8MCFLt9.jpg)

# FastBrainFuck
The original brainfuck language specification can be found [here](https://github.com/brain-lang/brainfuck/blob/master/brainfuck.md), this executable takes brainfuck code and collapses a lot of statements down to either single value instructions or two value instructions. For example, it will take the code `<<<<<<<<<<` and collapse it down to a single `<` followed by a 10 in binary. So it kind of becomes a BF++ type of thing.

# Internal Language Specification

Keep in mind that these value's are raw data, thus binary values. So this version of the language isn't meant to be "keyboard programmable". Instead, normal brainfuck code is compiled under the hood to the following language specification:
- `+ <amount>` Adds 'amount' to the current head 
- `- <amount>` Subtracts 'amount' from the current head
- `> <amount>` Moves head 'amount' tiles to the left
- `< <amount>` Moves head 'amount' tiles to the right 
- `[ <jump index>` Moves the instruction pointer to 'jump index' if head is zero
- `] <jump index>` Moves the instruction pointer to 'jump index' if head is not zero
- `,` Ask user for numeric value
- `.` Print value at head out as character value
- `m <amount>` Moves a value to another cell `<amount>` cells relative of head. (`[->+<]`)
- `e` Resets value at head to zero. (`[-]`)
- `j <amount>` Jumps head 'amount' steps until zero is found (`[<<<]`)

This is then interpreted in a normal brainfuck like manner and increases the speed of the runtime approximately 3 fold using the [mandelbrot](https://github.com/erikdubbelboer/brainfuck-jit/blob/master/mandelbrot.bf) file as benchmark.

# How to use?
run the exe as:
```
../Brainfuck.exe
```

Without any given parameters the brainfuck should run the [mandelbrot](https://github.com/erikdubbelboer/brainfuck-jit/blob/master/mandelbrot.bf) file and render out a nice cute little mandelbrot!

## CLI Parameters and flags
> **-fn [path]**
> 
> Filepath towards a brainfuck file containing brainfuck code.

> **-uo**
>
> Runs the brainfuck code in a traditional unoptimized brainfuck interpreter.

> **-t**
>
> Prints out the time it took from the start of the program till finish.

> **-r**
>
> Races a version of the unoptimized code vs the optimized code sequentially. When combined with `-t` the code will display the times and time difference between the two.
> This flag overrules `-uo`

> **-rainbow**
>
> Will make the console print in rainbow colors depending on the character value.


# Roadmap Language Extensions
- [x] Add support to simplify Empty Value (`[-]`) 
- [x] Add support to simplify Move (add) Value (`[->+<]`)
- [ ] Add support to simplify Multiplication (`++++[->++++<]`)
- [x] Add support to simplify Memory Hopping (`[<]`)
