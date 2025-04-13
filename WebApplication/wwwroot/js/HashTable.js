import { LinkedList } from './LinkedList.js'

class HashTable {
    constructor(size) {
        this.data = new Array(size)
    }

    set = (key, value) => {
        var address = this.$_hash(key)
        if (!this.data[address]) {
            //this.data[address] = []
            this.data[address] = new LinkedList({ key, value })
        } else {
            this.data[address].append({ key, value })
        }
        //this.data[address].push([ key, value ])
    }

    get = (key) => {
        var address = this.$_hash(key)
        var currentBasket = this.data[address]
        if (currentBasket.length) {

            // LinkedList
            let node = currentBasket.head
            while (node !== null) {
                if (node.value?.key === key) {
                    return node.value?.value
                }
                node = node.next
            }
            return undefined

            // Array style
            //for (let i = 0; i < currentBasket.length; i++) {
            //    if (currentBasket[i][0] === key)
            //        return currentBasket[i][1]
            //}
        }

        return undefined
    }

    keys = () => {
        let keys = []
        for (let i = 0; i < this.data.length; i++) {
            // Array
            //for (let j = 0; j < this.data[i].length; j++) {
            //    keys.push(this.data[i][j][0])
            //}

            // ListedList
            if (this.data[i] != undefined) {
                let node = this.data[i].head
                while (node !== null) {
                    keys.push(node.value?.key)
                    node = node.next
                }
            }
        }
        return keys
    }

    $_hash(key) {
        let hash = 0
        for (let i = 0; i < key.length; i++) {
            hash = (hash + key.charCodeAt(i) * i) % this.data.length
        }
        return hash
    }

    demo = () => {
        const myHashTable = new HashTable(2)
        myHashTable.set('grapes', 10000)
        myHashTable.set('banana', 20000)
        myHashTable.set('orange', 30000)
        let grapes = myHashTable.get('orange')
        console.log(grapes)
        console.log(myHashTable)
        console.log(myHashTable.keys())
    }
}

class Study {
    /**
     * @param {number[]} input
     * @return {number}
     */
    firstRecurringCharacter = (input) => {
        let firstRecurring;
        let minRecurrenceIndex = input.length; // Initialize with the maximum possible index

        // Outer loop: iterate through each element
        for (let i = 0; i < input.length; i++) {
            console.log("Outer loop index i:", i, "Value:", input[i]);

            // Inner loop: check for a matching element after the current index
            for (let j = i + 1; j < input.length; j++) {
                console.log("  Inner loop index j:", j, "Comparing:", input[i], "with", input[j]);

                if (input[i] === input[j]) {
                    console.log("    Match found for value", input[i], "at index", j);

                    // If the match is found earlier than any previous match, update the result
                    if (j < minRecurrenceIndex) {
                        console.log("    Updating firstRecurring to", input[i], "with second occurrence at index", j);
                        minRecurrenceIndex = j;
                        firstRecurring = input[i];
                    }
                    // Once a match is found for this i, break out of the inner loop
                    console.log("    Breaking inner loop for index i =", i);
                    break;
                }
            }
        }

        console.log("First recurring element is:", firstRecurring, "with second occurrence at index", minRecurrenceIndex);
        return firstRecurring;
    }

    /**
     * @param {number[]} input
     * @return {number}
     */
    firstRecurringCharacter2 = (input) => {
        let seen = new Set()
        for (let i = 0; i < input.length; i++) {
            if (seen.has(input[i])) {
                return input[i]
            }
            seen.add(input[i])
        }
        return undefined
    }
}
class App {
    run = () => {
        new HashTable().demo()
        //let result = new Study().firstRecurringCharacter([2, 5, 1, 1, 5, 2])
        //console.log(`Result: ${result}`)
    }
}

export { HashTable, App }