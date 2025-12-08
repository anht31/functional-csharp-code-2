var reverseString = function(s) {
    if (s.length <= 1) return
    // Suppose a first index, and b last characters in the array s
    let a = 0
    let b = s.length - 1
    while (s[a] && s[b] && a < b) {
        [s[a], s[b]] = [s[b], s[a]]
        a++
        b--
    }
}


// topic: Arrays & Hashing - Hash Map / Pair Summing
/**
 * @param {number[]} nums
 * @param {number} target
 * @return {number[]}
 */
var twoSum = function(nums, target) {
    const seen = new Map()
    for (let i = 0; i < nums.length; i++) {
        const complement = target - nums[i]
        if (seen.has(nums[i]))
            return [seen.get(nums[i]), i]
        seen.set(complement, i)
    }
};

class Demo {
    reverseString() {
        let s = ["h","e","l","l","o"]
        reverseString(s)
        console.log(s)
    }

    twoSum() {
        let nums = [2,7,11,15]
        let result = twoSum(nums, 9)
        console.log(result)
    }
}

class App {
    run() {
        new Demo().twoSum()
    }
}

export {App}