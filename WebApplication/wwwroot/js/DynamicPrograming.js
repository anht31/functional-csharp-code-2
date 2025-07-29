/**
 * @param {number[]} nums
 * @return {number}
 */

function plan() {
    let cache = {}
    let calculation = 0
    return function rob(nums, index) {
        if (nums.length === 0 || index >= nums.length) return 0
        if (nums.length === 1) return nums[0]
        if (index === nums.length - 1) return nums[index]
        console.log(++calculation)

        let a = nums[index] ?? 0
        let b = nums[index + 1] ?? 0

        let robSubA = cache[index + 2] ?? rob(nums, index + 2)
        cache[index + 2] ??= robSubA
        let robSubB = cache[index + 3] ?? rob(nums, index + 3)
        cache[index + 3] ??=  robSubB
        let robSubC = cache[index + 4] ?? rob(nums, index + 4)
        cache[index + 4] ??= robSubC

        let resultA = a + (robSubA > robSubB ? robSubA : robSubB)
        let resultB = b + (robSubB > robSubC ? robSubB : robSubC)

        return resultA > resultB ? resultA : resultB
    }
}

function houseRobber() {
    let nums = [2, 100, 1, 1, 100, 2]
    const planRob = plan()
    console.log(planRob(nums, 0))
}


/**
 * @param {number[]} prices
 * @return {number}
 */
var maxProfit = function (prices) {
    if (prices.length < 2) return 0

    let minPrice = prices[0]
    let maxProfit = 0

    for (let i = 1; i < prices.length; i++) {
        maxProfit = Math.max(maxProfit, prices[i] - minPrice)
        //console.log(prices[i], maxProfit, prices[i] - minPrice)
        minPrice = Math.min(minPrice, prices[i])
    }
    return maxProfit
};


// linear recursion
function CalcProfitLR() {
    return function maxProfitR(prices, minPrice, index = 0) {
        if (prices.length < 2 || index >= prices.length) return 0

        minPrice = Math.min(minPrice, prices[index])
        let maxProfit = Math.max(prices[index] - minPrice, maxProfitR(prices, minPrice, index + 1))

        return maxProfit
    }
}

// Dynamic programing


let calculate = 0

/**
 * @param {number} n
 * @return {number}
 */
function Climb() {
    const cache = {}
    return function climbStairs(n) {
        if (cache.hasOwnProperty(n)) {
            return cache[n]
        }

        calculate++

        if (n <= 1) return cache[n] = 1

        return cache[n] = climbStairs(n - 1) + climbStairs(n - 2)
    } 

}

class App {
    run() {
        const maxProfitR = CalcProfitLR()
        const prices = [7, 1, 5, 3, 6, 4, 9]
        //const prices = [7, 6, 4, 3, 1]
        console.log(maxProfitR([...prices], prices[0]))
        //console.log('show calc: ', calculate)

        //const n = 4
        //const climbStairs = new Climb()
        //console.log('\n', climbStairs(n))
        //console.log('calculate: ', calculate)
    }
}   

export { App }