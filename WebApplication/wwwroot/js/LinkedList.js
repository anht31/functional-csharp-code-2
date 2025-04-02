
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

class Node {
    constructor(value) {
        this.value = value
        this.next = null
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

    insert(index, value) {
        if (index < 0) return
        if (index > this.length) {
            this.append(value)
            return
        }
        if (index == 0) {
            this.prepend(value)
            return
        }

        const leader = this.traverseToIndex(index - 1)
        const newNode = { value, next: null }

        // 1 -> [newNode] -> 2
        const holdingPoint = leader.next
        leader.next = newNode
        newNode.next = holdingPoint
        this.length++
    }

    remove(index) {
        if (index < 0 || index >= this.length)
            return

        if (index === 0) {
            this.head = this.head.next
            return
        }

        console.log(this.head)

        const leader = this.traverseToIndex(index - 1)
        //const holdingPoint = leader.next?.next
        //delete leader.next // no need delete, because no pointer wilL GC
        const unwantedNode = leader.next
        leader.next = unwantedNode.next
        this.length--
    }

    traverseToIndex(index) {
        let currentNode = this.head
        let currentIndex = 0
        while (currentIndex < index && currentNode != null) {
            currentNode = currentNode.next
            currentIndex++
        }
        return currentNode
    }

    printList() {
        const array = []
        let currentNode = this.head
        while (currentNode !== null) {
            array.push(currentNode.value)
            currentNode = currentNode.next
        }
        return array
    }

    demo() {
        // 1 -> 10 --> 5 --> 16
        let myLinkedList = new LinkedList(10)
        myLinkedList.append(5)
        myLinkedList.append(16)
        myLinkedList.prepend(1)
        console.log(myLinkedList.printList())

        // 1 -> 10 --> [99] --> 5 --> 16
        myLinkedList.insert(2, 99)
        console.log(myLinkedList.printList())

        console.log(myLinkedList)
        myLinkedList.remove(2)
        console.log(myLinkedList.printList())

    }
}

class DoubleLinkedList {
    constructor(value) {
        this.head = {
            value: value,
            next: null,
            prev: null
        }
        this.tail = this.head // ref to current tail (use this as single access for LinkedList)
        this.length = 1
    }

    append(value) {
        const newNode = { value, next: null, prev: null }
        const current = this.tail
        current.next = newNode
        newNode.prev = current
        this.tail = newNode
        this.length++
    }

    prepend(value) {
        const newNode = { value, next: null, prev: null }
        const nextNode = this.head
        newNode.next = nextNode
        nextNode.prev = newNode

        this.head = newNode
        this.length++
    }

    insert(index, value) {
        if (index < 0) return
        if (index > this.length) {
            this.append(value)
            return
        }
        if (index == 0) {
            this.prepend(value)
            return
        }

        const leader = this.traverseToIndex(index - 1)
        const newNode = { value, next: null }

        // 1 -> [newNode] -> 2
        const holdingPoint = leader.next
        leader.next = newNode
        newNode.next = holdingPoint
        this.length++
    }

    remove(index) {
        if (index < 0 || index >= this.length)
            return

        if (index === 0) {
            this.head = this.head.next
            return
        }

        console.log(this.head)

        const leader = this.traverseToIndex(index - 1)
        //const holdingPoint = leader.next?.next
        //delete leader.next // no need delete, because no pointer wilL GC
        const unwantedNode = leader.next
        leader.next = unwantedNode.next
        this.length--
    }

    traverseToIndex(index) {
        let currentNode = this.head
        let currentIndex = 0
        while (currentIndex < index && currentNode != null) {
            currentNode = currentNode.next
            currentIndex++
        }
        return currentNode
    }

    printList() {
        const array = []
        let currentNode = this.head
        while (currentNode !== null) {
            array.push(currentNode.value)
            currentNode = currentNode.next
        }
        return array
    }

    demo() {
        // 1 -> 10 --> 5 --> 16
        let myLinkedList = new DoubleLinkedList(10)
        myLinkedList.append(5)
        myLinkedList.append(16)
        myLinkedList.prepend(1)
        console.log(myLinkedList.printList())
        console.log(myLinkedList)

        // 1 -> 10 --> [99] --> 5 --> 16
        //myLinkedList.insert(2, 99)
        //console.log(myLinkedList.printList())

        //console.log(myLinkedList)
        //myLinkedList.remove(2)
        //console.log(myLinkedList.printList())

    }
}


class App {
    run = () => {
        new DoubleLinkedList().demo()
    }
}

export { LinkedList, App }