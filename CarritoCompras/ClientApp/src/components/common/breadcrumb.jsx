import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

function Breadcrumb( props ) {
    const { title, articulos, adClass, type = "normal", slug = "default", container = "container", articuloId, ...parent } = props;
    let path = [];
    let x, prevArticulos, prevArticulo, currentArticulos, nextArticulos, nextArticulo;

    for ( x in parent ) {
        if ( 'function' !== typeof parent[ x ] )
            path.push( parent[ x ] );
    }

    currentArticulos = articulos.filter( item => item.id === parseInt( articuloId ) );

    // get articulo for prev button.
    prevArticulos = articulos.filter( item => item.id < parseInt( articuloId ) );
    if ( !prevArticulos || !prevArticulos.length )
        prevArticulo = currentArticulos[ 0 ];
    else
        prevArticulo = prevArticulos[ prevArticulos.length - 1 ];

    // get articulo for next button.
    nextArticulos = articulos.filter( item => item.id > parseInt( articuloId ) );
    if ( !nextArticulos || nextArticulos.length === 0 )
        nextArticulo = currentArticulos[ 0 ];
    else
        nextArticulo = nextArticulos[ 0 ];

    return (
        <nav aria-label="breadcrumb" className={ `breadcrumb-nav ${adClass}` }>
 
                <div className={ container }>
                    <ol className="breadcrumb">
                        <li className="breadcrumb-item"><Link to={ `${process.env.PUBLIC_URL}` }>Inicio</Link></li>
                        { path.map( item => (
                            <li className="breadcrumb-item" key={ item[ 0 ] }>
                                <Link to={ `${process.env.PUBLIC_URL}/${item[ 1 ]}` }>{ item[ 0 ] }</Link>
                            </li>
                        ) ) }
                        <li className="breadcrumb-item active" aria-current="page">{ title }</li>
                    </ol>
                </div>
        </nav>
    );
}

function mapStateToProps( state ) {
    return {
        //articulos: state.data.articulos ? state.data.articulos : []
        articulos:  []
    }
};

export default connect( mapStateToProps )( Breadcrumb );