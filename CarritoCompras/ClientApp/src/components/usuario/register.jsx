import React from 'react';
import { Link } from 'react-router-dom';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { Helmet } from 'react-helmet';
import PageHeader from '../common/page-header';
import Breadcrumb from '../common/breadcrumb';
import { usuariosService} from '../../api';
import {alertService} from '../../actions'

import LocationSearchInput from "../location_search_input/index";
import {Alert} from '../alert'


import IconButton from '@material-ui/core/IconButton';
import OutlinedInput from '@material-ui/core/OutlinedInput';
import InputLabel from '@material-ui/core/InputLabel';
import InputAdornment from '@material-ui/core/InputAdornment';
import FormControl from '@material-ui/core/FormControl';
import Visibility from '@material-ui/icons/Visibility';
import VisibilityOff from '@material-ui/icons/VisibilityOff';
import { makeStyles } from "@material-ui/core/styles";
import './form-control.css'

function Register({ history }) {
    const initialValues = {      
            email: '',
            password: '',
            confirmacionPassword: '',
            razonSocial: '',
            cuit: '',
            aceptaTerminos: true,
            location: {
                value: '',
                address: ''
            },
            telefono: '',
            nombre: '',
            apellido: '',    
            showPassword_password: false,
            showPassword_confirmacionPassword : false
      }
    


    function onSubmit(fields, { setStatus, setSubmitting }) {
      const campos = 
      {
        aceptaTerminos: fields.aceptaTerminos, //esta seteado en true mas arriba
        apellido: fields.apellido,
        cuit: fields.cuit,
        direccionDescripcion: fields.location.address,
        direccionValor: fields.location.value,
        email: fields.email,
        lat: fields.location.coordinates.lat.toString(),
        lng: fields.location.coordinates.lng.toString(),
        nombre: fields.nombre,
        password: fields.password,       
        razonSocial: fields.razonSocial,
        telefono: fields.telefono,
        
      }
      setStatus();
      usuariosService.register(campos)
          .then(() => {
              
             alertService.success('Registro exitoso, cont??ctese con el administrador para que active su cuenta', { keepAfterRouteChange: true });
             const timeout = setTimeout(() => {
                history.push('login');
              }, 3000);
          })
          .catch(error => {
              setSubmitting(false);
              alertService.error(error);
          });
    }


    

    const useStyles = makeStyles(theme => ({
        customHoverFocus: {
            "&:hover, &.Mui-focusVisible": { backgroundColor: "transparent" }
        },
        style_button: {
            "&.btn-primary" : {
                backgroundColor: "#094293 !important", /*agregar !importantpara anular los estados */
                borderColor: "#094293 !important" /*agregar !importantpara anular los estados */
             },
            "&:hover, &:active,&:visited,&:focus" : {
                backgroundColor: "#0b52b8 !important", /*agregar !importantpara anular los estados */
                borderColor: "#0b52b8 !important" /*agregar !importantpara anular los estados */
            }
        }
    }));

  
    const classes = useStyles();
    

    const validationSchema = Yup.object().shape({
        nombre: Yup.string()
            .required('Se requiere el nombre')
            .max(50, 'Maximo 50 caracteres'),
        apellido: Yup.string()
            .required('Se requiere el apellido')
            .max(50, 'Maximo 50 caracteres'),
        email: Yup.string()
            .email('Email no es valido')
            .required('Email es requerido')
            .max(50, 'Maximo 50 caracteres'),
        password: Yup.string()
            // .matches(
            //     "^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
            //     "M??nimo ocho caracteres, al menos una letra y un n??mero"
            // )
            .min(8, 'La contrase??a debe tener al menos 8 caracteres')
            .required('Contrase??a es requerida')
            .max(15, 'Maximo 15 caracteres'),
        confirmacionPassword: Yup.string()
            .when('password', (password, schema) => {
                if (password) return schema.required('Confirmaci??n de contrase??a es requerida');
            })          
            .oneOf([Yup.ref('password'), null], 'Las contrase??as deben coincidir')
            // .matches(
            //     "^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
            //     "M??nimo ocho caracteres, al menos una letra y un n??mero"
            // )
            .min(8, 'La contrase??a debe tener al menos 8 caracteres')
            .required('Contrase??a es requerida')
            .max(15, 'Maximo 15 caracteres'),
        // aceptaTerminos: Yup.bool()
        //     .oneOf([true], 'Acepta T??rminos & Condiciones es requerido'),
        razonSocial: Yup.string()
            .required('Se requiere razon social')
            .max(50, 'Maximo 50 caracteres'),
        cuit: Yup.string()
            // .matches(/^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/, 'Solo se aceptan n??meros')
            .max(11, 'Maximo 11 caracteres')
            .min(11, 'Minimo 11 caracteres')
            .required('Se requiere cuit'),
        telefono: Yup.string()
            // .matches(/^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/, 'Solo se aceptan n??meros')
            .max(11, 'Maximo 11 caracteres')
            .min(8, 'Minimo 8 caracteres')
            .required('Se requiere tel??fono'),          
        location: Yup.object().shape({
                value: Yup.string().required("Direcci??n es requerida").max(200, 'Maximo 200 caracteres'),
                address: Yup.string().required("Direcci??n invalida").max(200, 'Maximo 200 caracteres')
              }),
         
    });



    return( 

        <>
                <Helmet>
                    <title>Encendido Alsina - Registrarse</title>
                </Helmet>
                
                <h1 className="d-none">Encendido Alsina - Registrarse</h1>
                
                <div className="main">
                    <PageHeader  subTitle="Registrarse" />
                    <Breadcrumb title="Registrarse" parent1={ [ "Iniciar Sesi??n", "usuario/login" ] } />
                
                    <div className="page-content">
                            <div className="container">
                                <Alert />
                                <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
                                    {({ errors, touched, isSubmitting , setFieldValue,values }) => (
                                        <Form> 
                                            <h3 className="card-header">Registrar</h3>
                                            <div className="card-body">
                                                    <div className="form-group">
                                                        <label>Nombre</label>
                                                        <Field name="nombre" type="text" className={'form-control' + (errors.nombre && touched.nombre ? ' is-invalid' : '')} />
                                                        <ErrorMessage name="nombre" component="div" className="invalid-feedback" />
                                                    </div>
                                                    <div className="form-group">
                                                        <label>Apellido</label>
                                                        <Field name="apellido" type="text" className={'form-control' + (errors.apellido && touched.apellido ? ' is-invalid' : '')} />
                                                        <ErrorMessage name="apellido" component="div" className="invalid-feedback" />
                                                    </div>
                                                    <div className="form-group">
                                                        <label>Razon Social</label>
                                                        <Field name="razonSocial" type="text" className={'form-control' + (errors.razonSocial && touched.razonSocial ? ' is-invalid' : '')} />
                                                        <ErrorMessage name="razonSocial" component="div" className="invalid-feedback" />
                                                    </div>
                                                    <div className="form-group">
                                                        <label>Cuit </label>
                                                        <Field 
                                                            name="cuit" 
                                                            type="text" 
                                                            className={'form-control' + (errors.cuit && touched.cuit ? ' is-invalid' : '')}
                                                            values={values.cuit}
                                                            onChange={e => {
                                                                const re = /^[0-9\b]+$/;
                                                                if (e.target.value === '' || re.test(e.target.value)) {
                                                                    setFieldValue("cuit",  e.target.value)
                                                                }
                                                            }}
                                                        />
                                                        <ErrorMessage name="cuit" component="div" className="invalid-feedback" />
                                                    </div>
                                                    <div className="form-group">
                                                        <label>Direccion</label>
                                                        {/* paso id={0} porque no existe el usuario todavia , ese id hace referencia al id del usuario  */}
                                                        <Field name="location" component={LocationSearchInput} id={0} className={'form-control' + (errors.location && touched.location && touched.location.value  ? ' is-invalid' : '')}/> 
                                                        {errors.location && errors.location.value && <ErrorMessage name='location.value'  component="div" className="invalid-feedback" />}
                                                        {errors.location && !errors.location.value && errors.location.address && <ErrorMessage name='location.address'  component="div" className="invalid-feedback" />}
                                                    </div>
                                                    <div className="form-group">
                                                        <label>Telefono </label>
                                                        <Field 
                                                            name="telefono" 
                                                            type="text" 
                                                            className={'form-control' + (errors.telefono && touched.telefono ? ' is-invalid' : '')}
                                                            values={values.telefono}
                                                            onChange={e => {
                                                                const re = /^[0-9\b]+$/;
                                                                if (e.target.value === '' || re.test(e.target.value)) {
                                                                    setFieldValue("telefono",  e.target.value)
                                                                }
                                                            }}
                                                        />
                                                        <ErrorMessage name="telefono" component="div" className="invalid-feedback" />
                                                    </div>
                                                    <div className="form-group">
                                                        <label>Email</label>
                                                        <Field name="email" type="text" className={'form-control' + (errors.email && touched.email ? ' is-invalid' : '')} />
                                                        <ErrorMessage name="email" component="div" className="invalid-feedback" />
                                                    </div>
                                                    <div className="form-row">
                                                        <div className="form-group col">
                                                            <label>Contrase??a</label>
                                                            <IconButton 
                                                                aria-label="toggle password visibility"
                                                                onClick={ () => setFieldValue('showPassword_password', !values.showPassword_password)}
                                                                edge="end"
                                                                className={classes.customHoverFocus}
                                                            >
                                                                {values.showPassword_password ? 
                                                                                            <label 
                                                                                                    style={{margin:'20px' ,fontWeight: 'bold',color:'red'}}
                                                                                            >Ocultar contrase??a</label> 
                                                                                        : 
                                                                                            <label style={{margin:'20px' ,fontWeight: 'bold',color:'blue'}}
                                                                                            >Mostrar contrase??a</label>
                                                                    }
                                                                {values.showPassword_password ?   <Visibility  /> : <VisibilityOff />}
                                                            </IconButton>
                                                            <Field name="password"  type={values.showPassword_password ? 'text' : 'password'}  className={'form-control' + (errors.password && touched.password ? ' is-invalid' : '')}/>
                                                            <ErrorMessage name="password" component="div" className="invalid-feedback" />
                                                        </div>
                                                        <div className="form-group col">
                                                            <label>Confirmar Contrase??a</label>
                                                            <IconButton 
                                                                aria-label="toggle password visibility"
                                                                onClick={ () => setFieldValue('showPassword_confirmacionPassword', !values.showPassword_confirmacionPassword)}
                                                                edge="end"
                                                                className={classes.customHoverFocus}
                                                            >
                                                                {values.showPassword_confirmacionPassword ? 
                                                                                            <label 
                                                                                                    style={{margin:'20px' ,fontWeight: 'bold',color:'red'}}
                                                                                            >Ocultar contrase??a</label> 
                                                                                        : 
                                                                                            <label style={{margin:'20px' ,fontWeight: 'bold',color:'blue'}}
                                                                                            >Mostrar contrase??a</label>
                                                                    }
                                                                {values.showPassword_confirmacionPassword ?   <Visibility  /> : <VisibilityOff />}
                                                            </IconButton>
                                                            <Field name="confirmacionPassword"  type={values.showPassword_confirmacionPassword ? 'text' : 'password'}  className={'form-control' + (errors.confirmacionPassword && touched.confirmacionPassword ? ' is-invalid' : '')}/>
                                                            <ErrorMessage name="confirmacionPassword" component="div" className="invalid-feedback" />
                                                        </div>
                                                    </div>
                                                    {/* <div className="form-group form-check">
                                                        <Field type="checkbox" name="aceptaTerminos" id="aceptaTerminos" className={'form-check-input ' + (errors.aceptaTerminos && touched.aceptaTerminos ? ' is-invalid' : '')} />
                                                        <label htmlFor="aceptaTerminos" className="form-check-label">Acepta T??rminos & Condiciones</label>
                                                        <ErrorMessage name="aceptaTerminos" component="div" className="invalid-feedback" />
                                                    </div> */}
                                                    <div className="form-group">
                                                        <button type="submit" disabled={isSubmitting} className={`btn btn-primary ${classes.style_button}`}>
                                                            {isSubmitting && <span className="spinner-border spinner-border-sm mr-1"></span>}
                                                            Registrar
                                                        </button>
                                                        <Link to="login" className="btn btn-link">Cancelar</Link>
                                                    </div>
                                            </div>
                                        </Form>
                                        )}
                                </Formik>
                            </div>
                    </div>
                </div>
        </>
       )
      
    }
    
export default React.memo( Register );
