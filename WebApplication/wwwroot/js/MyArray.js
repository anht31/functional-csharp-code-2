
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

class Study {
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
}

class Exercises {
    /**
     * @param {number[]} nums
     * @param {number} target
     * @return {number[]}
     */
    twoSum (nums, target) {
        let lookup = new Map()

        for (let i = 0; i < nums.length; i++) {
            if (lookup.has(nums[i])) {
                return [lookup.get(nums[i]), i]
            }
            lookup.set(target - nums[i], i)
        }

        return []
    }

    /**
     * @param {number[]} nums
     * @return {number}
     */
    maxSubArray(nums) {
        // Initialize currentSum and maxSum with the first element of the array.
        let currentSum = nums[0];
        let maxSum = nums[0];
        console.log(`Index: 0 | Element: ${nums[0]} | Current Sum: ${currentSum} | Max Sum: ${maxSum}`);

        // Loop through the array starting from the second element.
        for (let i = 1; i < nums.length; i++) {
            const currentElement = nums[i];
            const prevCurrentSum = currentSum;
            // Decide whether to start a new subarray at the current element or extend the previous subarray.
            currentSum = Math.max(currentElement, prevCurrentSum + currentElement);
            // Update maxSum if the new currentSum is greater.
            maxSum = Math.max(maxSum, currentSum);

            console.log(`Index: ${i} | Element: ${currentElement} | Prev Current Sum: ${prevCurrentSum} | Updated Current Sum: ${currentSum} | Max Sum: ${maxSum}`);
        }

        console.log(`Final result: ${maxSum}`);
        return maxSum;
    }

    /**
     * @param {number[]} nums
     * @return {void} Do not return anything, modify nums in-place instead.
     */
    moveZeroes = function (nums) {
        let lastNonZeroFoundAt = 0;
        // Log initial state of the array.
        console.log("Initial array:", nums);

        // Traverse the array and move non-zero elements forward.
        for (let i = 0; i < nums.length; i++) {
            console.log(`Iteration ${i}: current element is ${nums[i]}, lastNonZeroFoundAt is ${lastNonZeroFoundAt}`);
            if (nums[i] !== 0) {
                console.log(`  -> Element ${nums[i]} is non-zero, move it to index ${lastNonZeroFoundAt}`);
                nums[lastNonZeroFoundAt] = nums[i];
                lastNonZeroFoundAt++;
                console.log("  -> Array state after moving element:", nums);
            } else {
                console.log("  -> Element is zero, do nothing");
            }
        }

        // Log state before filling zeros.
        console.log("After moving non-zero elements, lastNonZeroFoundAt is", lastNonZeroFoundAt);

        // Fill the remaining positions with zeros.
        for (let i = lastNonZeroFoundAt; i < nums.length; i++) {
            console.log(`Filling index ${i} with zero`);
            nums[i] = 0;
            console.log("  -> Array state after filling zero:", nums);
        }

        // Log final state of the array.
        console.log("Final array:", nums);
    }

    /**
     * @param {number[]} nums
     * @return {boolean}
     */
    containsDuplicate = function (nums) {
        let seen = new Set()
        for (let i = 0; i < nums.length; i++) {
            if (seen.has(nums[i])) {
                return true
            }
            seen.add(nums[i])
            console.log(seen)
        }
        return false
    }

    /**
     * @param {number[]} nums
     * @param {number} k
     * @return {void} Do not return anything, modify nums in-place instead.
     */
    rotate = function (nums, k) {
        for (let j = 0; j < k; j++) {
            let lastItem = nums[nums.length - 1]
            for (let i = nums.length - 1; i > 0; i--) {
                nums[i] = nums[i - 1]
            }
            nums[0] = lastItem
        }
    }

    /**
     * @param {number[]} nums
     * @param {number} k
     * @return {void} Do not return anything, modify nums in-place instead.
     */
    rotate2 = function (nums, k) {
        let length = nums.length
        k = k % length // Adjust k if k is greater than length
        let movingArray = nums.splice(0, length - k)
        nums.push(...movingArray)
    }
}

class App {
    run = () => {
        var exercises = new Exercises()
        let input = [1, 2, 3, 4, 5, 6, 7]
        let result = exercises.rotate2(input, 3)
        console.log(`Expect -> [5,6,7,1,2,3,4]; Return -> ${input}`)
    }
}

export { MyArray, App, Exercises }