import React from 'react';
import { Helmet } from 'react-helmet';

// import Custom Components
import PageHeader from '../../common/page-header';
import Breadcrumb from '../../common/breadcrumb';
import ArticuloDevExpresList from '../../features/articulos/articulo-dev-express-list';

function Catalogo( props ) {
    let grid = props.match.params.grid; //estas props vienen de components\home\index.jsx , linea "<Link  to={{pathname: `${process.env.PUBLIC_URL}/catalogo/list`, marca: cat.txtDescMarca}}>"
    let pathImgMarca = props.location.pathImgMarca;   //estas props vienen de components\home\index.jsx , linea "<Link  to={{pathname: `${process.env.PUBLIC_URL}/catalogo/list`, marca: cat.txtDescMarca}}>"
    const titles = { "list": "Encendido Alsina" };

    if ( grid !== "list") {
        window.location = process.env.PUBLIC_URL + "/pages/404";
    }

    return (
        <>
            <Helmet>
                <title>Encendido Alsina - Catalogo</title>
            </Helmet>
            
            <h1 className="d-none">Encendido Alsina - Catalogo</h1>

            <div className="main">
                <PageHeader  subTitle="Catalogo" />
                <Breadcrumb  parent1={ [ "Catalogo", "catalogo/list" ] } adClass="mb-2" />

                <div className="page-content">
                    <div className="container">
                        <div>
                            {/* esta opcion usa Dev Express */}
                            <div>
                                <ArticuloDevExpresList 
                                    column={ grid } 
                                    pathImgMarca = {pathImgMarca} />
                            </div>

                           

                          
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default React.memo( Catalogo );