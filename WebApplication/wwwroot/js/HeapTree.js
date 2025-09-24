
class HeapTree {
    maxHeapify(heap, size, i) {
        console.log(i)

        const left = i * 2 + 1
        const right = i * 2 + 2
        let largest = i

        largest = left < size && heap[left] > heap[largest] ? left : largest
        largest = right < size && heap[right] > heap[largest] ? right : largest

        if (largest != i) {
            [heap[i], heap[largest]] = [heap[largest], heap[i]]
            this.maxHeapify(heap, size, largest)
        }
    }
}

class App {
    run() {
        const heap = [16, 4, 10, 14, 7, 9, 3, 2, 8, 1]
        const heapTree = new HeapTree()
        heapTree.maxHeapify(heap, 9, 0)
    }
}

export { App }