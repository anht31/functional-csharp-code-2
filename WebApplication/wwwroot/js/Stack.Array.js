class Node {
    constructor(value) {
        this.value = value;
        this.next = null;
    }
}

class Stack {
    constructor() {
        this.items = []
        this.length = 0;
    }

    peek() {
        return this.items[this.length - 1]
    }

    push(value) {
        this.items[this.length++] = value
    }

    pop() {
        const index = this.length > 0 ? --this.length : -1
        const hasNumber = index >= 0
        const result = hasNumber ? this.items[index] : null
        hasNumber && delete this.items[index]
        return result
    }
}

class App {
    run() {
        const myStack = new Stack();
        myStack.push('google')
        myStack.push('udemy')
        myStack.push('discord')
        console.log(myStack)
        console.log(myStack.peek())
        console.log(myStack.pop())
        console.log(myStack.pop())
        console.log(myStack) // why this time this.top is null
        console.log(myStack.pop())
        console.log(myStack.pop())
        console.log(myStack)
    }
}

export { App }




//class Node {
//    constructor(value) {
//        this.value = value;
//        this.next = null;
//    }
//}

//class Stack {
//    constructor() {
//        this.items = []
//        this.length = 0;
//    }

//    peek() {
//        return this.items[this.length - 1]
//    }

//    push(value) {
//        this.length++
//        this.items.push(value)
//    }

//    pop() {
//        this.length++
//        return this.items.pop()
//    }

//    $isEmpty = () => this.top === null
//}

//class App {
//    run() {
//        const myStack = new Stack();
//        myStack.push('google')
//        myStack.push('udemy')
//        myStack.push('discord')
//        console.log(myStack)
//        console.log(myStack.peek())
//        console.log(myStack.pop())
//        console.log(myStack.pop())
//        console.log(myStack) // why this time this.top is null
//        console.log(myStack.pop())
//        console.log(myStack.pop())
//        console.log(myStack)
//    }
//}

//export { App }