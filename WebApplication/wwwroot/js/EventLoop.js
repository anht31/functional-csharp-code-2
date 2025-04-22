// ------------ demo.js ------------
/**
 * Hàm A có 3 đoạn:
 *  A1 – chạy ngay
 *  A2 – chạy ở microtask (sau khi Call Stack trống)
 *  A3 – chạy ở macrotask (tick kế tiếp)
 */
async function A() {
    let a = 123
    console.log('A1');

    await Promise.resolve();            // ← rời Call Stack → microtask queue

    console.log('A2' + a);

    await new Promise(r => setTimeout(r, 0)); // ← rời Call Stack → macrotask queue

    console.log('A3');
}

/**
 * Hàm B có 2 đoạn:
 *  B7 – chạy ngay
 *  B8 – chạy ở microtask (cùng tick với A2, sau A2 vì B gọi sau A)
 */
async function B() {
    console.log('B7');

    await Promise.resolve();            // ← rời Call Stack → microtask queue

    console.log('B8');
}

class App {
    run() {
        console.log('=== start ===');
        A();                                   // gọi trước
        B();                                   // gọi sau
        console.log('=== after call ===');
    }
}

export { App }