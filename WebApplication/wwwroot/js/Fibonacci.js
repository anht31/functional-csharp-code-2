
// 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 ...
class Fibonacci {
    fibonacciIterative(n) {
        if (n < 0) return undefined
        if (n === 0) return 0
        if (n === 1) return 1

        let first = 0;
        let second = 1;
        let result = 0
        for (let i = 2; i <= n; i++) {
            result = first + second
            first = second
            second = result
        }
        return result
    }

    fibonacciRecursive(n) {
        //console.log(n)
        if (n < 0) return undefined
        if (n === 0) return 0
        if (n === 1) return 1

        return this.fibonacciRecursive(n - 1) + this.fibonacciRecursive(n - 2)
    }
}

class App {
    run() {
        const fibonacci = new Fibonacci()
        const iterative = fibonacci.fibonacciIterative(8)
        console.log(`Iterative: ${iterative}`)

        const recursive = fibonacci.fibonacciRecursive(4)
        console.log(`Recursive: ${recursive}`)
    }
}

export { App }