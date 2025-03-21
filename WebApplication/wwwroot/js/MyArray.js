
class MyArray {
    constructor() {
        this.index = 0
        this.data = {}
    }
    get = index => this.data[index]
    push = item => this.data[this.index++] = item
    pop = () => {
        let lastItem = this.data[this.index - 1]
        delete this.data[--this.index]
        return lastItem
    }
    delete = (index) => {
        if (index >= this.index) return
        delete this.data[index]
        this.#shiftItemsLeft(index)
    }
    insert = (index, item) => {
        if (index > this.index) return
        this.#shiftItemsRight(index)
        this.data[index] = item
    }
    show = () => {
        for (let i = 0; i < this.index; i++)
            console.log(`${i}: ${this.data[i]}`)
    }

    #shiftItemsLeft = (index) => {
        for (let i = index; i < this.index - 1; i++) {
            this.data[i] = this.data[i + 1]
        }
        delete this.data[--this.index]
    }
    #shiftItemsRight = (index) => {
        for (let i = this.index; i > index; i--) {
            this.data[i] = this.data[i - 1]
        }
        this.index++
    }
}

class App {

    $testArray = () => {
        var myArray = new MyArray()
        myArray.push('a')
        myArray.push('b')
        myArray.push('c')
        myArray.push('d')
        myArray.push('e')

        myArray.insert(2, 'x')
        myArray.show()
        console.log(myArray)

    }

    $reverse(str) {
        if (!str || str.length < 2 || typeof str !== 'string')
            return 'hmm that is not good'

        let result = []
        for (let i = str.length - 1; i >= 0; i--) {
            result.push(str[i])
        }
        return result.join('')
    }

    $reverse2(str) {
        return str.split('').reverse().join('')
    }

    $reverse3 = (str) => [...str].reverse().join('')

    $mergeSortedArrays = (arrayA, arrayB) => {
        const mergedArray = []
        let itemA = arrayA.shift()
        let itemB = arrayB.shift()

        if (arrayA.length == 0) return arrayB
        if (arrayB.length == 0) return arrayA

        while (itemA !== undefined || itemB !== undefined) {
            if (itemA <= itemB) {
                mergedArray.push(itemA)
                itemA = arrayA.shift()
            } else if (itemA > itemB) {
                mergedArray.push(itemB)
                itemB = arrayB.shift()
            } else {
                mergedArray.push(itemA ?? itemB)
                break
            }
        }

        return mergedArray
    }

    $mergeSortedArrays2 = (arrayI, arrayJ) => {
        const mergedArray = []
        let itemI = arrayI[0]
        let itemJ = arrayJ[0]
        let i = 0
        let j = 0

        if (arrayI.length == 0) return arrayJ
        if (arrayJ.length == 0) return arrayI

        while (itemI || itemJ) {
            console.log(`${itemI} - ${itemJ}`)
            if (!itemJ || itemI < itemJ) {
                mergedArray.push(itemI)
                itemI = arrayI[++i]
            }
            else {
                mergedArray.push(itemJ)
                itemJ = arrayJ[++j]
            }
        }
        console.log(mergedArray)
    } 

    run = () => {
        var result = this.$mergeSortedArrays2([0, 3, 4, 31], [4, 6, 30, 32])
        console.log(result)
    }
}

export { MyArray, App }