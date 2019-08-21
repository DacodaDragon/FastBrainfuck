# FastBrainFuck
Originally handles brainfuck code accoring to brainfuck specifications that can be found [here](https://github.com/brain-lang/brainfuck/blob/master/brainfuck.md), but overhauls the code by "source to source" compiling to generate two value instructions where the first value is the operation identifier and the second value is how much time the operation should be repeated or some other value helping optimize. 

# Internal Fast Brain Fuck Format

Keep in mind that these value's are raw data. So this version of the language isn't "keyboard programmable" instead, normal brainfuck code is compiled under the hood to the next language specification:
- `+ <amount>` Adds 'amount' to the current cell 
- `- <amount>` Subtracts 'amount' from the current cell
- `> <amount>` Moves head 'amount' tiles to the left
- `< <amount>` Moves head 'amount' tiles to the right 
- `[ <jump index>` Moves head to 'jump index' if value of head is zero
- `] <jump index>` Moves head to 'jump index' if value of head is not zero
- `,` Ask user for numeric value
- `.` Print head out as character value


# Roadmap Language Extensions
- Add support for Empty Value (`[-]`) 
- Add support for Move Value (`[->+<]`)
- Add support for multiplication (`++[->++<]`)
