import { ADD_TO_CART, REMOVE_FROM_CART, CHANGE_QTY, CHANGE_SHIPPING, REFRESH_STORE } from "../constants/action-types";
import { findIndex } from "../utils";
import { persistReducer } from "redux-persist";
import storage from 'redux-persist/lib/storage';

const initialState = {
    cart: [],
    shipping: "free"
}

function cartReducer( state = initialState, action ) {
    switch ( action.type ) {
        case ADD_TO_CART:
            const articuloId = action.articulo.id;

            if ( findIndex( state.cart, articulo => articulo.id === articuloId ) !== -1 ) {
                const cart = state.cart.reduce( ( cartAcc, articulo ) => {
                    if ( articulo.id === articuloId ) {
                        cartAcc.push( { ...articulo, qty: parseInt( articulo.qty ) + parseInt( action.qty ), sum: ( articulo.precioLista_por_coeficiente_por_medioIva ) * ( parseInt( articulo.qty ) + parseInt( action.qty ) ) } ) // Increment qty
                    } else {
                        cartAcc.push( articulo )
                    }
                    return cartAcc
                }, [] )

                return { ...state, cart }
            }

            return {
                ...state,
                cart: [
                    ...state.cart,
                    {
                        ...action.articulo,
                        qty: action.qty,
                        sum: (  action.articulo.precioLista_por_coeficiente_por_medioIva ) * action.qty
                    }
                ]
            }

        case REMOVE_FROM_CART:
            return {
                ...state,
                cart: state.cart.filter( item => item.id !== action.articuloId )
            };

        case CHANGE_QTY:
            const cart = state.cart.reduce( ( cartAcc, articulo ) => {
                if ( articulo.id === action.articuloId ) {
                    cartAcc.push( { ...articulo, qty: action.qty, sum: (  articulo.precioLista_por_coeficiente_por_medioIva ) * action.qty } ) // Increment qty
                } else {
                    cartAcc.push( articulo )
                }
                return cartAcc;
            }, [] )

            return { ...state, cart };

        case REFRESH_STORE:
            return { ...state, cart: [], shipping: "free" };

        case CHANGE_SHIPPING:
            return { ...state, shipping: action.shipping };

        default:
            return state;
    }
}

const persistConfig = {
    keyPrefix: "molla-",
    key: "cartlist",
    storage
}

export default persistReducer( persistConfig, cartReducer );