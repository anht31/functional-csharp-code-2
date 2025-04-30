class Node {
    constructor(value) {
        this.value = value;
        this.next = null;
    }
}

class Queue {
    constructor() {
        this.first = null;
        this.last = null;
        this.length = 0;
    }
    peek() {
        return this.first
    }
    enqueue(value) {
        const node = new Node(value)
        this.first ??= node
        if (this.last == null) {
            this.last = node
        } else {
            const holdingPoint = this.last
            holdingPoint.next = node
            this.last = node
        }
        this.length++
    }
    dequeue() {
        if (this.first === null) return null
        //console.log(this)
        console.log(structuredClone(this))
        const node = this.first
        const nextNode = node.next
        this.first = nextNode
        this.last = nextNode === null ? null: this.last
        this.length--
        return node
    }
    //isEmpty;
}

const myQueue = new Queue();

//Joy
//Matt
//Pavel
//Samir

// Joy -> Matt -> Pavel -> Samir

// Stack: LIFO
// Queue: FIFO

class App {
    run() {
        const queue = new Queue()
        queue.enqueue('Joy')
        queue.enqueue('Matt')
        queue.enqueue('Pavel')
        queue.enqueue('Samir')

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