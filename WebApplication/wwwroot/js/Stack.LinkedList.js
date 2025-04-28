class Node {
    constructor(value) {
        this.value = value;
        this.next = null;
    }
}

class Stack {
    constructor() {
        this.top = null;
        this.bottom = null;
        this.length = 0;
    }

    peek() {
        return !this.$isEmpty() ? this.top?.value : null 
    }

    push(value) {
        const previewNode = this.top
        const newNode = new Node(value)
        newNode.next = previewNode
        this.top = newNode
        this.bottom ??= newNode
        this.length++
    }

    pop() {
        if (this.$isEmpty()) return null

        const node = this.top
        const nextNode = node.next
        this.top = nextNode
        this.bottom = nextNode === null ? null : this.bottom
        this.length--

        return node?.value
    }

    $isEmpty = () => this.top === null
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