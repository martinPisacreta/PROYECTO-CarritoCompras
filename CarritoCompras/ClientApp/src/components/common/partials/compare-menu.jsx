import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { removeFromCompare, resetCompare } from '../../../actions';
import { safeContent } from '../../../utils';

function CompareMenu( props ) {
    const { compareList, removeFromCompare, resetCompare } = props;

    return (
        <div className="dropdown compare-dropdown">
            {/* <a href="#dropdown" className="dropdown-toggle" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" data-display="static" title="Compare Articulos" aria-label="Compare Articulos">
                <i className="icon-random"></i>
                <span className="compare-txt">Comparar</span>
            </a> */}
            <div className="dropdown-menu dropdown-menu-right">
                <ul className="compare-articulos">
                    { compareList.map( ( item, index ) => (
                        <li className="compare-articulo" key={ index }>
                            <button className="btn-remove" title="Remove Articulo" onClick={ () => removeFromCompare( item ) }><i className="icon-close"></i></button>
                            <h4 className="compare-articulo-title"><Link to={ `${process.env.PUBLIC_URL}/articulo/default/27` } dangerouslySetInnerHTML={ safeContent( item.codigoArticulo ) }></Link></h4>
                        </li>
                    ) ) }
                    {
                        0 === compareList.length ? <p className="mb-1">No hay articulos para comparar</p> : ''
                    }
                </ul>
                <div className="compare-actions">
                    <button className="action-link" onClick={ () => resetCompare() }>Limpiar todo</button>
                    <Link to="#" className="btn btn-outline-primary-2"><span>Comparar</span><i className="icon-long-arrow-right"></i></Link>
                </div>
            </div>
        </div>
    );
}

function mapStateToProps( state ) {
    return {
        compareList: state.compare.items ? state.compare.items : []
    }
}
export default connect( mapStateToProps, { removeFromCompare, resetCompare } )( CompareMenu );