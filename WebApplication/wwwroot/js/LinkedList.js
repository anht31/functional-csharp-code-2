
class LinkedList2 {

    constructor(value, tail) {
        this.value = value
        this.tail = tail
    }


    demo() {
        // 10 --> 5 --> 16
        let myLinkedList = new LinkedList2(10, new LinkedList2(5, new LinkedList2(16, null)))
        console.log(myLinkedList)
    }
}
class LinkedList3 {

    constructor(value) {
        this.head = this.current = new LinkedList3.Node(value, null)
        this.length = 1
    }

    append(value) {
        this.current.tail = new LinkedList3.Node(value, null)
        this.current = this.current.tail
        this.length++
    }

    demo() {
        // 10 --> 5 --> 16
        let myLinkedList = new LinkedList3(10)
        myLinkedList.append(5)
        myLinkedList.append(16)
        console.log(myLinkedList.head)
    }
}
LinkedList3.Node = class {
    constructor(value, tail) {
        this.head = value
        this.tail = tail
    }
}

class LinkedList {
    constructor(value) {
        this.head = {
            value: value,
            next: null
        }
        this.tail = this.head // ref to current tail (use this as single access for LinkedList)
        this.length = 1
    }

    append(value) {
        const newNode = { value, next: null }
        const current = this.tail
        current.next = newNode
        this.tail = newNode
        this.length++
    }

    prepend(value) {
        const newNode = { value, next: null }
        newNode.next = this.head
        this.head = newNode
        this.length++
    }

    demo() {
        // 10 --> 5 --> 16
        let myLinkedList = new LinkedList(10)
        myLinkedList.append(5)
        myLinkedList.append(16)
        myLinkedList.prepend(1)
        console.log(myLinkedList)
    }
}


class App {
    run = () => {
        new LinkedList().demo()
    }
}

export { LinkedList, App }