import { createSlice, type PayloadAction } from "@reduxjs/toolkit";

const initState = {
    value: 0
};

const countSlice = createSlice({
    name: "count",
    initialState: initState,
    reducers: {
        increment(state) {
            state.value++;
        },
        decrement(state) {
            state.value--;
        },
        plus(state, action: PayloadAction<number>) {
            state.value += action.payload;
        },
        minus(state, action: PayloadAction<number>) {
            state.value -= action.payload;
        }
    },
});

export const { increment, decrement, plus, minus } = countSlice.actions;
export default countSlice.reducer;
