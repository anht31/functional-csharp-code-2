
class Factorial {
    findFactorialRecursive(number) {
        if (number < 1) return undefined
        if (number === 1) return number

        return number * this.findFactorialRecursive(--number)
    }

    findFactorialIterative(number) {
        if (number < 1) return undefined
        if (number === 1) return number

        let result = number
        for (let i = number - 1; i > 1; i--) {
            result = result * i
        }
        return result
    }
}

class App {
    run() {
        const factorial = new Factorial()
        const recursive = factorial.findFactorialRecursive(5)
        const iterative = factorial.findFactorialIterative(5)

        console.log(`Recursive: ${recursive}`)
        console.log(`iterative: ${iterative}`)
    }
}

export { App }