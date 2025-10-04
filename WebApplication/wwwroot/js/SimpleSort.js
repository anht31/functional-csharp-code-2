
function bubbleSort(array) {
    //Code here
    if (!array || array.length < 2) return array
    let maxIndex = array.length - 1

    for (let limit = maxIndex; limit > 0; limit--) {
        for (let a = 0; a < limit; a++) {
            let b = a + 1
            //console.log(`${a}-${b}`)
            if (array[a] > array[b])
                [array[a], array[b]] = [array[b], array[a]] // swap
        }
        
    }
}

function SelectionSort(array) {

    for (let i = 0; i < array.length; i++) {
        let iMin = i
        for (let j = i + 1; j < array.length; j++) {
            if (array[iMin] > array[j]) {
                iMin = j
            }
        }
        [array[i], array[iMin]] = [array[iMin], array[i]] // swap
    }
}

function InsertionSort(array) {
    for (let i = 1; i < array.length; i++) {
        for (let j = i; j > 0; j--) {
            let pre = j - 1
            if (array[pre] < array[j]) break

            [array[j], array[pre]] = [array[pre], array[j]] // swap
        }
    }
}

class App {
    run() {
        const numbers = [99, 44, 6, 2, 1, 5, 63, 87, 283, 4, 0];
        //const numbers = [1, 2, 3, 8, 5, 6, 7, 9, 11, 12, 10]
        InsertionSort(numbers);
        console.log(numbers);
    }
}

export { App }