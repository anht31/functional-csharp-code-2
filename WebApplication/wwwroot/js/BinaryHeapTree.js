

// Max Heap
class BinaryHeapTree {
    constructor() {
        this.heap = []
    }

    insert(value) {
        if (this.heap.length === 0) {
            this.heap.push(value)
            return
        }

        const parentIndex = Math.floor((this.heap.length - 1) / 2)
        this.heap.push(value)
        const childIndex = this.heap.length - 1
        this.$bubbleUp(childIndex, parentIndex)
    }

    dequeue() {
        const maxHeap = this.heap[0]
        const lastItem = this.heap.pop()
        if (this.heap.length === 1)
            return maxHeap

        this.heap[0] = lastItem
        this.$heapifyDown(0)

        return maxHeap
    }

    remove(value) {
        const removeIndex = this.$getIndex(value)
        if (removeIndex === null) return

        const lastItem = this.heap.pop()
        if (this.heap.length === 0) return

        this.heap[removeIndex] = lastItem

        const parentIndex = Math.floor((removeIndex - 1) / 2)
        const isBubbleUp = parentIndex >= 0 && this.heap[removeIndex] > this.heap[parentIndex]
        if (isBubbleUp) {
            this.$bubbleUp(removeIndex, parentIndex)
        } else {
            this.$heapifyDown(removeIndex)
        }
    }

    $getIndex(value) {
        if (value === null) return
        for (let i = 0; i < this.heap.length; i++) {
            if (this.heap[i] === value) {
                return i
            }
        }
        return null
    }


    $bubbleUp(childIndex, parentIndex) {
        if (parentIndex < 0) return

        const parent = this.heap[parentIndex]
        const child = this.heap[childIndex]
        if (child > parent) {
            this.heap[parentIndex] = child
            this.heap[childIndex] = parent
            const nextParentIndex = Math.floor((parentIndex - 1) / 2)
            this.$bubbleUp(parentIndex, nextParentIndex)
        }
    }

    $heapifyDown(index) {
        const leftIndex = index*2 + 1
        const rightIndex = index*2 + 2
        const maxIndex = this.heap.length - 1

        const leftIndexOutOfRange = leftIndex > maxIndex
        if (leftIndexOutOfRange) return

        const parent = this.heap[index]
        const left = this.heap[leftIndex]
        const rightIndexOutOfRange = rightIndex > maxIndex
        if (rightIndexOutOfRange) {
            if (left > parent) {
                this.heap[index] = left
                this.heap[leftIndex] = parent
            }
            return
        }

        const right = this.heap[rightIndex]
        const isParentLargerThanChild = parent >= left || parent >= right
        if (isParentLargerThanChild) return

        if (left > right) {
            this.heap[index] = left
            this.heap[leftIndex] = parent
            this.$heapifyDown(leftIndex)
        } else {
            this.heap[index] = right
            this.heap[rightIndex] = parent
            this.$heapifyDown(rightIndex)
        }
    }
}

class App {
    run() {
        const binaryHeap = new BinaryHeapTree()
        binaryHeap.insert(9)
        binaryHeap.insert(5)
        binaryHeap.insert(8)
        binaryHeap.insert(2)
        binaryHeap.insert(1)
        binaryHeap.insert(6)

        console.log(structuredClone(binaryHeap.heap))

        const value = binaryHeap.remove(2)
        console.log(structuredClone(binaryHeap.heap))
    }
}

export { App }