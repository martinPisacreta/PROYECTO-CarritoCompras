import React from 'react';
import { Link } from 'react-router-dom';

// import Utils
import { safeContent } from '../../utils';

export default function Slider( props ) {
    const { slider } = props;
    let image = process.env.PUBLIC_URL + '/' + slider.image;

    return (
        <div className="intro-slide" style={{ backgroundImage: `url(${image})`, backgroundSize: '100% 100%'}  } key={ slider.image }>
            <div className="container intro-content">
                <div className="row">
                    <div className="col-auto offset-lg-3 intro-col">
                        {/* <h3 className="intro-subtitle">{ slider.subtitle }</h3>
                        <h1 className="intro-title" dangerouslySetInnerHTML={ safeContent( slider.title ) }></h1>

                        <Link to={ `${process.env.PUBLIC_URL}/catalogo/list` } className="btn btn-outline-primary-2">
                            <span>Carrito de Compras Now</span>
                            <i className="icon-long-arrow-right"></i>
                        </Link>  */}
                    </div>
                </div>
            </div>
        </div>
    )
}