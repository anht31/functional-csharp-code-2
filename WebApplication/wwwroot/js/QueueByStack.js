import { Stack } from './Stack.Array.js'

class Queue {
    constructor() {
        this.inStack = new Stack()
        this.outStack = new Stack()
        this.length = 0
    }

    peek() {
        if (this.outStack.length > 0)
            return this.outStack.peek()

        if (this.traverseStack())
            return this.outStack.peek()
    }

    enqueue(value) {
        this.inStack.push(value)
    }

    dequeue() {
        if (this.outStack.length > 0)
            return this.outStack.pop()

        if (this.traverseStack())
            return this.outStack.pop()
    }

    traverseStack() {
        if (this.inStack.length === 0) return false

        while (this.inStack.length > 0) {
            const item = this.inStack.pop()
            this.outStack.push(item)
        }

        return true
    }
}

// outStack:
// 4
// 3

// inStack:
// 1
// 2

class App {
    run() {
        const queue = new Queue()
        queue.enqueue('Joy')
        queue.enqueue('Matt')
        queue.enqueue('Pavel')
        queue.enqueue('Samir')

        console.log(structuredClone(queue))
        console.log('Begin dequeue:')
        console.log(queue.peek())
        console.log(queue.dequeue())
        console.log(queue.dequeue())
        console.log(queue.dequeue())
        console.log(queue.dequeue())
        console.log(queue.dequeue())
    }
}

export { App }

