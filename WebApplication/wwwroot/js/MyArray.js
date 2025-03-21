
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

    }

    run = () => {
        var text = this.$reverse("How are you?")
        console.log(text)
    }
}

export { MyArray, App }