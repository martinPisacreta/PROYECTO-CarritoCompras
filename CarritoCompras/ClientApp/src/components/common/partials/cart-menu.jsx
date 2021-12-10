import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { getCartCount, getCartTotal } from '../../../services';
import { removeFromCart } from '../../../actions';
import { safeContent } from '../../../utils';
import { usuarioPedidosService , usuariosService } from '../../../api';

function CartMenu( props ) {
    const { cartlist, removeFromCart } = props;
    let total = getCartTotal( cartlist );
    const user = usuariosService.userValue;

    return (
        <div className="dropdown cart-dropdown">
            <Link to={ user ? `${process.env.PUBLIC_URL}/shop/cart` : `${process.env.PUBLIC_URL}/usuario/login`} className="dropdown-toggle" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" data-display="static">
                <i className="icon-shopping-cart"></i>
                <span className="cart-count">{ user ? getCartCount( cartlist ) : 0 }</span>
                <span className="cart-txt">Carrito</span>
            </Link>

            {
                user ?
                        <div className={ `dropdown-menu dropdown-menu-right ${cartlist.length === 0 ? 'text-center' : ''}` } >
                            {
                                0 === cartlist.length ?
                                    <p>No hay articulos en el carrito</p> :
                                    <>
                                        <div className="dropdown-cart-articulos">
                                            { cartlist.map( ( item, index ) => (
                                                <div className="articulo" key={ index }>
                                                    
                                                    <div className="articulo-cart-details">
                                                        <h4 className="articulo-title">
                                                            <Link to={ `${process.env.PUBLIC_URL}/articulo/default/7` } dangerouslySetInnerHTML={ safeContent( item.codigoArticulo ) }></Link>
                                                        </h4>

                                                        <span className="cart-articulo-info">
                                                            <span className="cart-articulo-qty">{ item.qty }</span>
                                                            x ${ item.precioLista_por_coeficiente_por_medioIva.toLocaleString( undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 } ) }
                                                        </span>
                                                    </div>

                                                    <figure className="articulo-image-container">
                                                        {/* <Link to={ `${process.env.PUBLIC_URL}/articulo/default/7` } className="articulo-image"> */}
                                                            

                                                            <img 
                                                                src={ process.env.PUBLIC_URL +  item.pathImagenArticulo ? item.pathImagenArticulo + '?' + Date.now() : '/assets/images/articulos/shop_encendido_alsina/sin_imagen.png'} 
                                                                data-oi={ process.env.PUBLIC_URL  + item.pathImagenArticulo ? item.pathImagenArticulo + '?' + Date.now() : '/assets/images/articulos/shop_encendido_alsina/sin_imagen.png'} alt="articulo" />
                                                        {/* </Link> */}
                                                    </figure>

                                                   
                                                    <button className="btn-remove"  title="Remove Articulo" onClick={ () => removeFromCart( item.id ) }><i className="icon-close"></i></button>
                                                     
                                                </div>
                                            ) ) }
                                        </div>
                                        <div className="dropdown-cart-total">
                                            <span>Total</span>

                                            <span className="cart-total-price">${ total.toLocaleString( undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 } ) }</span>
                                        </div>

                                        <div className="dropdown-cart-action">
                                            <Link to={ `${process.env.PUBLIC_URL}/shop/cart` } className="btn btn-primary">Ver carrito</Link>
                                            {/* <Link to={ `${process.env.PUBLIC_URL}/shop/checkout` } className="btn btn-outline-primary-2"><span>Pagar</span><i className="icon-long-arrow-right"></i></Link> */}
                                        </div>
                                    </>
                            }
                        </div>
                    :
                        null
            }
        </div>
    );
}

function mapStateToProps( state ) {
    return {
        cartlist: state.cartlist.cart ? state.cartlist.cart : []
    }
}

export default connect( mapStateToProps, { removeFromCart } )( CartMenu );