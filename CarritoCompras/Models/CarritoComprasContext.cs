using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CarritoCompras.Models
{
    public partial class CarritoComprasContext : DbContext
    {
       

        public CarritoComprasContext(DbContextOptions<CarritoComprasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<ArticuloHistorico> ArticuloHistoricos { get; set; }
        public virtual DbSet<ArticuloTmp> ArticuloTmps { get; set; }
        public virtual DbSet<ArticuloTmpDuplicado> ArticuloTmpDuplicados { get; set; }
        public virtual DbSet<ArticuloTmpErrore> ArticuloTmpErrores { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ClienteCuentaCorriente> ClienteCuentaCorrientes { get; set; }
        public virtual DbSet<ClienteDato> ClienteDatos { get; set; }
        public virtual DbSet<ClienteDir> ClienteDirs { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }
        public virtual DbSet<FacturaDetalleLog> FacturaDetalleLogs { get; set; }
        public virtual DbSet<FacturaLog> FacturaLogs { get; set; }
        public virtual DbSet<Familium> Familia { get; set; }
        public virtual DbSet<LogTareaProgramadum> LogTareaProgramada { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<ProveedorDato> ProveedorDatos { get; set; }
        public virtual DbSet<ProveedorDir> ProveedorDirs { get; set; }
        public virtual DbSet<Tcalle> Tcalles { get; set; }
        public virtual DbSet<Tmunicipio> Tmunicipios { get; set; }
        public virtual DbSet<Tpai> Tpais { get; set; }
        public virtual DbSet<Tprovincium> Tprovincia { get; set; }
        public virtual DbSet<TtipoCalle> TtipoCalles { get; set; }
        public virtual DbSet<TtipoCliente> TtipoClientes { get; set; }
        public virtual DbSet<TtipoCondicionFactura> TtipoCondicionFacturas { get; set; }
        public virtual DbSet<TtipoCondicionIva> TtipoCondicionIvas { get; set; }
        public virtual DbSet<TtipoCondicionPago> TtipoCondicionPagos { get; set; }
        public virtual DbSet<TtipoDato> TtipoDatos { get; set; }
        public virtual DbSet<TtipoDir> TtipoDirs { get; set; }
        public virtual DbSet<TtipoFactura> TtipoFacturas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuarioPedido> UsuarioPedidos { get; set; }
        public virtual DbSet<UsuarioPedidoDetalle> UsuarioPedidoDetalles { get; set; }
        public virtual DbSet<Vendedor> Vendedors { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => e.IdArticulo);

                entity.ToTable("articulo");

                entity.HasIndex(e => e.CodigoArticulo, "IDX_CODIGO_ARTICULO");

                entity.HasIndex(e => e.CodigoArticuloMarca, "IDX_CODIGO_ARTICULO_MARCA");

                entity.HasIndex(e => e.DescripcionArticulo, "IDX_DESCRIPCION_ARTICULO");

                entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");

                entity.Property(e => e.Accion)
                    .HasMaxLength(50)
                    .HasColumnName("accion");

                entity.Property(e => e.CodigoArticulo)
                    .HasMaxLength(100)
                    .HasColumnName("codigo_articulo");

                entity.Property(e => e.CodigoArticuloMarca)
                    .HasMaxLength(100)
                    .HasColumnName("codigo_articulo_marca");

                entity.Property(e => e.DescripcionArticulo)
                    .HasMaxLength(400)
                    .HasColumnName("descripcion_articulo");

                entity.Property(e => e.FecBaja)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_baja");

                entity.Property(e => e.FechaUltModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ult_modif");

                entity.Property(e => e.IdOrden).HasColumnName("id_orden");

                entity.Property(e => e.IdTablaFamilia).HasColumnName("id_tabla_familia");

                entity.Property(e => e.PathImg)
                    .HasMaxLength(400)
                    .HasColumnName("path_img");

                entity.Property(e => e.PrecioLista)
                    .HasColumnType("numeric(18, 4)")
                    .HasColumnName("precio_lista");

                entity.Property(e => e.SnOferta).HasColumnName("sn_oferta");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.IdTablaFamiliaNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdTablaFamilia)
                    .HasConstraintName("FK_articulo_familia");
            });

            modelBuilder.Entity<ArticuloHistorico>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("articulo_historico");

                entity.Property(e => e.Accion)
                    .HasMaxLength(50)
                    .HasColumnName("accion");

                entity.Property(e => e.CodigoArticulo)
                    .HasMaxLength(100)
                    .HasColumnName("codigo_articulo");

                entity.Property(e => e.CodigoArticuloMarca)
                    .HasMaxLength(100)
                    .HasColumnName("codigo_articulo_marca");

                entity.Property(e => e.DescripcionArticulo)
                    .HasMaxLength(400)
                    .HasColumnName("descripcion_articulo");

                entity.Property(e => e.FecBaja)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_baja");

                entity.Property(e => e.FechaUltModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ult_modif");

                entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");

                entity.Property(e => e.IdLista).HasColumnName("id_lista");

                entity.Property(e => e.IdOrden).HasColumnName("id_orden");

                entity.Property(e => e.IdTablaFamilia).HasColumnName("id_tabla_familia");

                entity.Property(e => e.PathImg)
                    .HasMaxLength(400)
                    .HasColumnName("path_img");

                entity.Property(e => e.PrecioLista)
                    .HasColumnType("numeric(18, 4)")
                    .HasColumnName("precio_lista");

                entity.Property(e => e.SnOferta).HasColumnName("sn_oferta");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.IdTablaFamiliaNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdTablaFamilia)
                    .HasConstraintName("FK_articulo_historico_familia");
            });

            modelBuilder.Entity<ArticuloTmp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("articulo_tmp");

                entity.Property(e => e.CodigoArticulo)
                    .HasMaxLength(255)
                    .HasColumnName("codigo_articulo");

                entity.Property(e => e.CodigoArticuloMarca)
                    .HasMaxLength(255)
                    .HasColumnName("codigo_articulo_marca");

                entity.Property(e => e.DescripcionArticulo)
                    .HasMaxLength(400)
                    .HasColumnName("descripcion_articulo");

                entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");

                entity.Property(e => e.IdOrden).HasColumnName("id_orden");

                entity.Property(e => e.IdTablaFamilia).HasColumnName("id_tabla_familia");

                entity.Property(e => e.PathImg)
                    .HasMaxLength(400)
                    .HasColumnName("path_img");

                entity.Property(e => e.PrecioFinal).HasColumnName("precio_final");

                entity.Property(e => e.PrecioLista).HasColumnName("precio_lista");

                entity.Property(e => e.SnOferta).HasColumnName("sn_oferta");

                entity.Property(e => e.Stock).HasColumnName("stock");
            });

            modelBuilder.Entity<ArticuloTmpDuplicado>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("articulo_tmp_duplicados");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoArticulo)
                    .HasMaxLength(255)
                    .HasColumnName("codigo_articulo");

                entity.Property(e => e.CodigoArticuloMarca)
                    .HasMaxLength(255)
                    .HasColumnName("codigo_articulo_marca");
            });

            modelBuilder.Entity<ArticuloTmpErrore>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("articulo_tmp_errores");

                entity.Property(e => e.CodigoArticulo)
                    .HasMaxLength(255)
                    .HasColumnName("codigo_articulo");

                entity.Property(e => e.CodigoArticuloMarca)
                    .HasMaxLength(255)
                    .HasColumnName("codigo_articulo_marca");

                entity.Property(e => e.DescripcionArticulo)
                    .HasMaxLength(400)
                    .HasColumnName("descripcion_articulo");

                entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");

                entity.Property(e => e.IdOrden).HasColumnName("id_orden");

                entity.Property(e => e.IdTablaFamilia).HasColumnName("id_tabla_familia");

                entity.Property(e => e.Observacion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("observacion");

                entity.Property(e => e.PathImg)
                    .HasMaxLength(400)
                    .HasColumnName("path_img");

                entity.Property(e => e.PrecioFinal).HasColumnName("precio_final");

                entity.Property(e => e.PrecioLista).HasColumnName("precio_lista");

                entity.Property(e => e.SnOferta).HasColumnName("sn_oferta");

                entity.Property(e => e.Stock).HasColumnName("stock");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("cliente");

                entity.Property(e => e.IdCliente)
                    .ValueGeneratedNever()
                    .HasColumnName("id_cliente");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("accion");

                entity.Property(e => e.FecUltModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_ult_modif");

                entity.Property(e => e.IdCondicionAnteIva).HasColumnName("id_condicion_ante_iva");

                entity.Property(e => e.IdCondicionFactura).HasColumnName("id_condicion_factura");

                entity.Property(e => e.IdCondicionPago).HasColumnName("id_condicion_pago");

                entity.Property(e => e.IdTipoCliente).HasColumnName("id_tipo_cliente");

                entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");

                entity.Property(e => e.NombreFantasia)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre_fantasia");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(100)
                    .HasColumnName("razon_social");

                entity.Property(e => e.SnActivo).HasColumnName("sn_activo");

                entity.HasOne(d => d.IdCondicionAnteIvaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdCondicionAnteIva)
                    .HasConstraintName("FK_cliente_ttipo_condicion_iva");

                entity.HasOne(d => d.IdCondicionFacturaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdCondicionFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_ttipo_condicion_factura");

                entity.HasOne(d => d.IdCondicionPagoNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdCondicionPago)
                    .HasConstraintName("FK_cliente_ttipo_condicion_pago");

                entity.HasOne(d => d.IdTipoClienteNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdTipoCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_ttipo_cliente");

                entity.HasOne(d => d.IdVendedorNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdVendedor)
                    .HasConstraintName("FK_cliente_vendedor");
            });

            modelBuilder.Entity<ClienteCuentaCorriente>(entity =>
            {
                entity.HasKey(e => e.IdClienteCuentaCorriente);

                entity.ToTable("cliente_cuenta_corriente");

                entity.Property(e => e.IdClienteCuentaCorriente).HasColumnName("id_cliente_cuenta_corriente");

                entity.Property(e => e.CodTipoFacturaVieja)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_tipo_factura_vieja");

                entity.Property(e => e.FechaFacturaVieja)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_factura_vieja");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.ImpFactura)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("imp_factura");

                entity.Property(e => e.NroFacturaVieja).HasColumnName("nro_factura_vieja");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(500)
                    .HasColumnName("observacion");

                entity.Property(e => e.Pago1)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("pago_1");

                entity.Property(e => e.Pago1Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("pago_1_fecha");

                entity.Property(e => e.Pago2)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("pago_2");

                entity.Property(e => e.Pago2Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("pago_2_fecha");

                entity.Property(e => e.Pago3)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("pago_3");

                entity.Property(e => e.Pago3Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("pago_3_fecha");

                entity.Property(e => e.Pago4)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("pago_4");

                entity.Property(e => e.Pago4Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("pago_4_fecha");
            });

            modelBuilder.Entity<ClienteDato>(entity =>
            {
                entity.HasKey(e => new { e.IdCliente, e.CodTipoDato })
                    .HasName("PK_cliente_conta");

                entity.ToTable("cliente_datos");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.CodTipoDato)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_tipo_dato");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("accion");

                entity.Property(e => e.FecUltModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_ult_modif");

                entity.Property(e => e.SnActivo).HasColumnName("sn_activo");

                entity.Property(e => e.TxtDatoCliente)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_dato_cliente");

                entity.HasOne(d => d.CodTipoDatoNavigation)
                    .WithMany(p => p.ClienteDatos)
                    .HasForeignKey(d => d.CodTipoDato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cliente_c_1730258338");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.ClienteDatos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_datos_cliente");
            });

            modelBuilder.Entity<ClienteDir>(entity =>
            {
                entity.HasKey(e => new { e.IdCliente, e.CodTipoDir });

                entity.ToTable("cliente_dir");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.CodTipoDir)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_tipo_dir");

                entity.Property(e => e.Accion)
                    .HasMaxLength(100)
                    .HasColumnName("accion");

                entity.Property(e => e.CodCalle)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("cod_calle");

                entity.Property(e => e.CodClaseDir).HasColumnName("cod_clase_dir");

                entity.Property(e => e.CodMunicipio)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("cod_municipio");

                entity.Property(e => e.CodPais)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_pais")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CodProvincia)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("cod_provincia");

                entity.Property(e => e.CodTipoCalle)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("cod_tipo_calle");

                entity.Property(e => e.FecUltModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_ult_modif");

                entity.Property(e => e.SnActivo).HasColumnName("sn_activo");

                entity.Property(e => e.TxtApto)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("txt_apto");

                entity.Property(e => e.TxtCodPostal)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("txt_cod_postal");

                entity.Property(e => e.TxtDireccion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion");

                entity.Property(e => e.TxtNumero)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("txt_numero");

                entity.Property(e => e.TxtPiso)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("txt_piso");

                entity.HasOne(d => d.CodCalleNavigation)
                    .WithMany(p => p.ClienteDirs)
                    .HasForeignKey(d => d.CodCalle)
                    .HasConstraintName("FK_cliente_dir_tcalle");

                entity.HasOne(d => d.CodMunicipioNavigation)
                    .WithMany(p => p.ClienteDirs)
                    .HasForeignKey(d => d.CodMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_dir_tmunicipio");

                entity.HasOne(d => d.CodPaisNavigation)
                    .WithMany(p => p.ClienteDirs)
                    .HasForeignKey(d => d.CodPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cliente__cod_p__15A53433");

                entity.HasOne(d => d.CodProvinciaNavigation)
                    .WithMany(p => p.ClienteDirs)
                    .HasForeignKey(d => d.CodProvincia)
                    .HasConstraintName("FK__cliente__cod_p__178D7CA5");

                entity.HasOne(d => d.CodTipoCalleNavigation)
                    .WithMany(p => p.ClienteDirs)
                    .HasForeignKey(d => d.CodTipoCalle)
                    .HasConstraintName("FK_cliente_dir_ttipo_calle");

                entity.HasOne(d => d.CodTipoDirNavigation)
                    .WithMany(p => p.ClienteDirs)
                    .HasForeignKey(d => d.CodTipoDir)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cliente___cod_t__3C24A6E0");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.ClienteDirs)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_dir_cliente");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK_Empresa");

                entity.ToTable("empresa");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("celular");

                entity.Property(e => e.Cuit)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("cuit");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FechaInicioActividad)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_inicio_actividad");

                entity.Property(e => e.IdCondicionAnteIva).HasColumnName("id_condicion_ante_iva");

                entity.Property(e => e.NombreFantasia)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre_fantasia");

                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("razon_social");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdCondicionAnteIvaNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.IdCondicionAnteIva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_empresa_ttipo_condicion_iva");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura)
                    .HasName("PK_cliente_factura");

                entity.ToTable("factura");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.CodTipoFactura)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_tipo_factura");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaSnEmitida)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_sn_emitida");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdCondicionFactura).HasColumnName("id_condicion_factura");

                entity.Property(e => e.NroFactura).HasColumnName("nro_factura");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(500)
                    .HasColumnName("observacion");

                entity.Property(e => e.PathFactura)
                    .HasMaxLength(1500)
                    .HasColumnName("path_factura");

                entity.Property(e => e.PrecioFinal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio_final");

                entity.Property(e => e.PrecioFinalConPagoMayorA30Dias)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio_final_con_pago_mayor_a_30_dias");

                entity.Property(e => e.PrecioFinalConPagoMenorA30Dias)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio_final_con_pago_menor_a_30_dias");

                entity.Property(e => e.PrecioFinalConPagoMenorA7Dias)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio_final_con_pago_menor_a_7_dias");

                entity.Property(e => e.SnEmitida).HasColumnName("sn_emitida");

                entity.Property(e => e.SnModificaPrecioFinal).HasColumnName("sn_modifica_precio_final");

                entity.Property(e => e.SnMostrarPagoMayor30Dias).HasColumnName("sn_mostrar_pago_mayor_30_dias");

                entity.Property(e => e.SnMostrarPagoMenor30Dias).HasColumnName("sn_mostrar_pago_menor_30_dias");

                entity.Property(e => e.SnMostrarPagoMenor7Dias).HasColumnName("sn_mostrar_pago_menor_7_dias");

                entity.HasOne(d => d.CodTipoFacturaNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.CodTipoFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura_ttipo_factura");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura_cliente");

                entity.HasOne(d => d.IdCondicionFacturaNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdCondicionFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura_ttipo_condicion_factura");
            });

            modelBuilder.Entity<FacturaDetalle>(entity =>
            {
                entity.HasKey(e => e.IdFacturaDetalle)
                    .HasName("PK_cliente_factura_detalle");

                entity.ToTable("factura_detalle");

                entity.Property(e => e.IdFacturaDetalle).HasColumnName("id_factura_detalle");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoArticulo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("codigo_articulo");

                entity.Property(e => e.CodigoArticuloMarca)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("codigo_articulo_marca");

                entity.Property(e => e.DescripcionArticulo)
                    .HasMaxLength(400)
                    .HasColumnName("descripcion_articulo");

                entity.Property(e => e.FecBaja)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_baja");

                entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.Iva)
                    .HasColumnType("numeric(18, 4)")
                    .HasColumnName("iva");

                entity.Property(e => e.PrecioListaXCoeficiente)
                    .HasColumnType("numeric(18, 4)")
                    .HasColumnName("precio_lista_x_coeficiente");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.FacturaDetalles)
                    .HasForeignKey(d => d.IdArticulo)
                    .HasConstraintName("FK_cliente_factura_detalle_articulo");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.FacturaDetalles)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_factura_detalle_cliente_factura");
            });

            modelBuilder.Entity<FacturaDetalleLog>(entity =>
            {
                entity.HasKey(e => e.IdFacturaDetalleLog);

                entity.ToTable("factura_detalle_log");

                entity.Property(e => e.IdFacturaDetalleLog).HasColumnName("id_factura_detalle_log");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("accion");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoArticulo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("codigo_articulo");

                entity.Property(e => e.CodigoArticuloMarca)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("codigo_articulo_marca");

                entity.Property(e => e.DescripcionArticulo)
                    .HasMaxLength(400)
                    .HasColumnName("descripcion_articulo");

                entity.Property(e => e.FecBaja)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_baja");

                entity.Property(e => e.FechaLog)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_log");

                entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.IdFacturaDetalle).HasColumnName("id_factura_detalle");

                entity.Property(e => e.Iva)
                    .HasColumnType("numeric(18, 4)")
                    .HasColumnName("iva");

                entity.Property(e => e.PrecioListaXCoeficiente)
                    .HasColumnType("numeric(18, 4)")
                    .HasColumnName("precio_lista_x_coeficiente");
            });

            modelBuilder.Entity<FacturaLog>(entity =>
            {
                entity.HasKey(e => e.IdFacturaLog);

                entity.ToTable("factura_log");

                entity.Property(e => e.IdFacturaLog).HasColumnName("id_factura_log");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("accion");

                entity.Property(e => e.CodTipoFactura)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_tipo_factura");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaLog)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_log");

                entity.Property(e => e.FechaSnEmitida)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_sn_emitida");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdCondicionFactura).HasColumnName("id_condicion_factura");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.NroFactura).HasColumnName("nro_factura");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(500)
                    .HasColumnName("observacion");

                entity.Property(e => e.PathFactura)
                    .HasMaxLength(1500)
                    .HasColumnName("path_factura");

                entity.Property(e => e.PrecioFinal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio_final");

                entity.Property(e => e.PrecioFinalConPagoMayorA30Dias)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio_final_con_pago_mayor_a_30_dias");

                entity.Property(e => e.PrecioFinalConPagoMenorA30Dias)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio_final_con_pago_menor_a_30_dias");

                entity.Property(e => e.PrecioFinalConPagoMenorA7Dias)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio_final_con_pago_menor_a_7_dias");

                entity.Property(e => e.SnEmitida).HasColumnName("sn_emitida");

                entity.Property(e => e.SnModificaPrecioFinal).HasColumnName("sn_modifica_precio_final");

                entity.Property(e => e.SnMostrarPagoMayor30Dias).HasColumnName("sn_mostrar_pago_mayor_30_dias");

                entity.Property(e => e.SnMostrarPagoMenor30Dias).HasColumnName("sn_mostrar_pago_menor_30_dias");

                entity.Property(e => e.SnMostrarPagoMenor7Dias).HasColumnName("sn_mostrar_pago_menor_7_dias");
            });

            modelBuilder.Entity<Familium>(entity =>
            {
                entity.HasKey(e => e.IdTablaFamilia);

                entity.ToTable("familia");

                entity.HasIndex(e => e.TxtDescFamilia, "IDX_TXT_DESC_FAMILIA");

                entity.Property(e => e.IdTablaFamilia)
                    .ValueGeneratedNever()
                    .HasColumnName("id_tabla_familia");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("accion");

                entity.Property(e => e.Algoritmo1)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("algoritmo_1");

                entity.Property(e => e.Algoritmo10)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("algoritmo_10");

                entity.Property(e => e.Algoritmo2)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("algoritmo_2");

                entity.Property(e => e.Algoritmo3)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("algoritmo_3");

                entity.Property(e => e.Algoritmo4)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("algoritmo_4");

                entity.Property(e => e.Algoritmo5)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("algoritmo_5");

                entity.Property(e => e.Algoritmo6)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("algoritmo_6");

                entity.Property(e => e.Algoritmo7)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("algoritmo_7");

                entity.Property(e => e.Algoritmo8)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("algoritmo_8");

                entity.Property(e => e.Algoritmo9)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("algoritmo_9");

                entity.Property(e => e.FecUltModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_ult_modif");

                entity.Property(e => e.IdFamilia).HasColumnName("id_familia");

                entity.Property(e => e.IdTablaMarca).HasColumnName("id_tabla_marca");

                entity.Property(e => e.PathImg)
                    .HasMaxLength(400)
                    .HasColumnName("path_img");

                entity.Property(e => e.SnActivo).HasColumnName("sn_activo");

                entity.Property(e => e.TxtDescFamilia)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("txt_desc_familia");

                entity.HasOne(d => d.IdTablaMarcaNavigation)
                    .WithMany(p => p.Familia)
                    .HasForeignKey(d => d.IdTablaMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_familia_marca");
            });

            modelBuilder.Entity<LogTareaProgramadum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("log_tarea_programada");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Observacion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("observacion");

                entity.Property(e => e.Tabla)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("tabla");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.IdTablaMarca);

                entity.ToTable("marca");

                entity.HasIndex(e => e.PathImg, "IDX_PATH_IMG");

                entity.Property(e => e.IdTablaMarca)
                    .ValueGeneratedNever()
                    .HasColumnName("id_tabla_marca");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("accion");

                entity.Property(e => e.FecUltModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_ult_modif");

                entity.Property(e => e.IdMarca).HasColumnName("id_marca");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.PathImg)
                    .HasMaxLength(400)
                    .HasColumnName("path_img");

                entity.Property(e => e.SnActivo).HasColumnName("sn_activo");

                entity.Property(e => e.TxtDescMarca)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("txt_desc_marca");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Marcas)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_marca_proveedor");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK__proveedor__79FD19BE");

                entity.ToTable("proveedor");

                entity.Property(e => e.IdProveedor)
                    .ValueGeneratedNever()
                    .HasColumnName("id_proveedor");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("accion");

                entity.Property(e => e.FecUltModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_ult_modif");

                entity.Property(e => e.IdCondicionAnteIva).HasColumnName("id_condicion_ante_iva");

                entity.Property(e => e.IdCondicionPago).HasColumnName("id_condicion_pago");

                entity.Property(e => e.PathImg)
                    .HasMaxLength(400)
                    .HasColumnName("path_img");

                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("razon_social");

                entity.Property(e => e.SnActivo).HasColumnName("sn_activo");

                entity.HasOne(d => d.IdCondicionAnteIvaNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.IdCondicionAnteIva)
                    .HasConstraintName("FK_proveedor_ttipo_condicion_iva");

                entity.HasOne(d => d.IdCondicionPagoNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.IdCondicionPago)
                    .HasConstraintName("FK_proveedor_ttipo_condicion_pago");
            });

            modelBuilder.Entity<ProveedorDato>(entity =>
            {
                entity.HasKey(e => new { e.IdProveedor, e.CodTipoDato });

                entity.ToTable("proveedor_datos");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.CodTipoDato)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_tipo_dato");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("accion");

                entity.Property(e => e.FecUltModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_ult_modif");

                entity.Property(e => e.SnActivo).HasColumnName("sn_activo");

                entity.Property(e => e.TxtDatoProveedor)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_dato_proveedor");

                entity.HasOne(d => d.CodTipoDatoNavigation)
                    .WithMany(p => p.ProveedorDatos)
                    .HasForeignKey(d => d.CodTipoDato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proveedor_datos_ttipo_dato");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.ProveedorDatos)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proveedor_datos_proveedor");
            });

            modelBuilder.Entity<ProveedorDir>(entity =>
            {
                entity.HasKey(e => new { e.IdProveedor, e.CodTipoDir });

                entity.ToTable("proveedor_dir");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.CodTipoDir)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_tipo_dir");

                entity.Property(e => e.Accion)
                    .HasMaxLength(100)
                    .HasColumnName("accion");

                entity.Property(e => e.CodCalle)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("cod_calle");

                entity.Property(e => e.CodClaseDir).HasColumnName("cod_clase_dir");

                entity.Property(e => e.CodMunicipio)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("cod_municipio");

                entity.Property(e => e.CodPais)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_pais")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CodProvincia)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("cod_provincia");

                entity.Property(e => e.CodTipoCalle)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("cod_tipo_calle");

                entity.Property(e => e.FecUltModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_ult_modif");

                entity.Property(e => e.SnActivo).HasColumnName("sn_activo");

                entity.Property(e => e.TxtApto)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("txt_apto");

                entity.Property(e => e.TxtCodPostal)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("txt_cod_postal");

                entity.Property(e => e.TxtDireccion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion");

                entity.Property(e => e.TxtNumero)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("txt_numero");

                entity.Property(e => e.TxtPiso)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("txt_piso");

                entity.HasOne(d => d.CodCalleNavigation)
                    .WithMany(p => p.ProveedorDirs)
                    .HasForeignKey(d => d.CodCalle)
                    .HasConstraintName("FK_proveedor_dir_tcalle");

                entity.HasOne(d => d.CodMunicipioNavigation)
                    .WithMany(p => p.ProveedorDirs)
                    .HasForeignKey(d => d.CodMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proveedor_dir_tmunicipio");

                entity.HasOne(d => d.CodPaisNavigation)
                    .WithMany(p => p.ProveedorDirs)
                    .HasForeignKey(d => d.CodPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__proveedor__cod_p__15A53433");

                entity.HasOne(d => d.CodProvinciaNavigation)
                    .WithMany(p => p.ProveedorDirs)
                    .HasForeignKey(d => d.CodProvincia)
                    .HasConstraintName("FK__proveedor__cod_p__178D7CA5");

                entity.HasOne(d => d.CodTipoCalleNavigation)
                    .WithMany(p => p.ProveedorDirs)
                    .HasForeignKey(d => d.CodTipoCalle)
                    .HasConstraintName("FK_proveedor_dir_ttipo_calle");

                entity.HasOne(d => d.CodTipoDirNavigation)
                    .WithMany(p => p.ProveedorDirs)
                    .HasForeignKey(d => d.CodTipoDir)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__proveedor___cod_t__3C24A6E0");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.ProveedorDirs)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proveedor_dir_proveedor");
            });

            modelBuilder.Entity<Tcalle>(entity =>
            {
                entity.HasKey(e => e.CodCalle)
                    .HasName("tcalle_9217673101");

                entity.ToTable("tcalle");

                entity.Property(e => e.CodCalle)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("cod_calle");

                entity.Property(e => e.TxtDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<Tmunicipio>(entity =>
            {
                entity.HasKey(e => e.CodMunicipio)
                    .HasName("PK_tmunicipio_1");

                entity.ToTable("tmunicipio");

                entity.Property(e => e.CodMunicipio)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("cod_municipio");

                entity.Property(e => e.CodDivipola)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("cod_divipola");

                entity.Property(e => e.CodPais)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_pais");

                entity.Property(e => e.CodProvincia)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("cod_provincia");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(255)
                    .HasColumnName("txt_desc");

                entity.HasOne(d => d.CodPaisNavigation)
                    .WithMany(p => p.Tmunicipios)
                    .HasForeignKey(d => d.CodPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmunicipio_tpais1");

                entity.HasOne(d => d.CodProvinciaNavigation)
                    .WithMany(p => p.Tmunicipios)
                    .HasForeignKey(d => d.CodProvincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tmunicipio_tprovincia");
            });

            modelBuilder.Entity<Tpai>(entity =>
            {
                entity.HasKey(e => e.CodPais)
                    .HasName("tpais_2115288061");

                entity.ToTable("tpais");

                entity.Property(e => e.CodPais)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_pais");

                entity.Property(e => e.TxtDesc)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<Tprovincium>(entity =>
            {
                entity.HasKey(e => e.CodProvincia);

                entity.ToTable("tprovincia");

                entity.Property(e => e.CodProvincia)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("cod_provincia");

                entity.Property(e => e.CodPais)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_pais");

                entity.Property(e => e.TxtDesc)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.HasOne(d => d.CodPaisNavigation)
                    .WithMany(p => p.Tprovincia)
                    .HasForeignKey(d => d.CodPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tprovincia_tpais");
            });

            modelBuilder.Entity<TtipoCalle>(entity =>
            {
                entity.HasKey(e => e.CodTipoCalle)
                    .HasName("ttipo_call_15520575841");

                entity.ToTable("ttipo_calle");

                entity.Property(e => e.CodTipoCalle)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("cod_tipo_calle");

                entity.Property(e => e.TxtDesc)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<TtipoCliente>(entity =>
            {
                entity.HasKey(e => e.IdTipoCliente);

                entity.ToTable("ttipo_cliente");

                entity.Property(e => e.IdTipoCliente)
                    .ValueGeneratedNever()
                    .HasColumnName("id_tipo_cliente");

                entity.Property(e => e.SnActivo).HasColumnName("sn_activo");

                entity.Property(e => e.TxtDesc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<TtipoCondicionFactura>(entity =>
            {
                entity.HasKey(e => e.IdCondicionFactura);

                entity.ToTable("ttipo_condicion_factura");

                entity.Property(e => e.IdCondicionFactura)
                    .ValueGeneratedNever()
                    .HasColumnName("id_condicion_factura");

                entity.Property(e => e.SnActivo).HasColumnName("sn_activo");

                entity.Property(e => e.TxtDesc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<TtipoCondicionIva>(entity =>
            {
                entity.HasKey(e => e.IdCondicionAnteIva);

                entity.ToTable("ttipo_condicion_iva");

                entity.Property(e => e.IdCondicionAnteIva)
                    .ValueGeneratedNever()
                    .HasColumnName("id_condicion_ante_iva");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("tipo");

                entity.Property(e => e.TxtDesc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtDescResumido)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("txt_desc_resumido");
            });

            modelBuilder.Entity<TtipoCondicionPago>(entity =>
            {
                entity.HasKey(e => e.IdCondicionPago);

                entity.ToTable("ttipo_condicion_pago");

                entity.Property(e => e.IdCondicionPago)
                    .ValueGeneratedNever()
                    .HasColumnName("id_condicion_pago");

                entity.Property(e => e.TxtDesc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<TtipoDato>(entity =>
            {
                entity.HasKey(e => e.CodTipoDato)
                    .HasName("ttipo_conta_5885781541");

                entity.ToTable("ttipo_dato");

                entity.Property(e => e.CodTipoDato)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_tipo_dato");

                entity.Property(e => e.TxtDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<TtipoDir>(entity =>
            {
                entity.HasKey(e => e.CodTipoDir)
                    .HasName("ttipo_dir_19360589521");

                entity.ToTable("ttipo_dir");

                entity.Property(e => e.CodTipoDir)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_tipo_dir");

                entity.Property(e => e.TxtDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<TtipoFactura>(entity =>
            {
                entity.HasKey(e => e.CodTipoFactura)
                    .HasName("ttipo_fact_5885781541");

                entity.ToTable("ttipo_factura");

                entity.Property(e => e.CodTipoFactura)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("cod_tipo_factura");

                entity.Property(e => e.Letra)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("letra")
                    .IsFixedLength(true);

                entity.Property(e => e.TxtDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_Usuario_1");

                entity.ToTable("usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.AceptaTerminos).HasColumnName("acepta_terminos");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("apellido");

                entity.Property(e => e.Cuit)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("cuit");

                entity.Property(e => e.DireccionDescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("direccion_descripcion");

                entity.Property(e => e.DireccionValor)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("direccion_valor");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FechaCreacionUsuario)
                    .HasColumnType("date")
                    .HasColumnName("fecha_creacion_usuario")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaUltimaModificacionUsuario)
                    .HasColumnType("date")
                    .HasColumnName("fecha_ultima_modificacion_usuario");

                entity.Property(e => e.Lat)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("lat");

                entity.Property(e => e.Lng)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("lng");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("password");

                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("razon_social");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("rol");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.Property(e => e.TokenReseteo)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("token_reseteo");

                entity.Property(e => e.TokenReseteoFechaExpiracion)
                    .HasColumnType("date")
                    .HasColumnName("token_reseteo_fecha_expiracion");

                entity.Property(e => e.TokenVerificacion)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("token_verificacion");

                entity.Property(e => e.UsuarioVerificado)
                    .HasColumnName("usuario_verificado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Utilidad).HasColumnName("utilidad");
            });

            modelBuilder.Entity<UsuarioPedido>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioPedido);

                entity.ToTable("usuario_pedido");

                entity.Property(e => e.IdUsuarioPedido).HasColumnName("id_usuario_pedido");

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_pedido");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Total)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.UsuarioPedidos)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuario_pedido_empresa");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioPedidos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuario_pedido_usuario");
            });

            modelBuilder.Entity<UsuarioPedidoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioPedidoDetalle);

                entity.ToTable("usuario_pedido_detalle");

                entity.Property(e => e.IdUsuarioPedidoDetalle).HasColumnName("id_usuario_pedido_detalle");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoArticulo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("codigo_articulo");

                entity.Property(e => e.Coeficiente)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("coeficiente");

                entity.Property(e => e.DescripcionArticulo)
                    .HasMaxLength(400)
                    .HasColumnName("descripcion_articulo");

                entity.Property(e => e.IdUsuarioPedido).HasColumnName("id_usuario_pedido");

                entity.Property(e => e.PrecioLista)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("precio_lista");

                entity.Property(e => e.PrecioListaPorCoeficientePorMedioIva)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("precioLista_por_coeficiente_por_medioIva");

                entity.Property(e => e.SnOferta).HasColumnName("sn_oferta");

                entity.Property(e => e.TxtDescFamilia)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("txt_desc_familia");

                entity.Property(e => e.TxtDescMarca)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("txt_desc_marca");

                entity.Property(e => e.Utilidad).HasColumnName("utilidad");

                entity.HasOne(d => d.IdUsuarioPedidoNavigation)
                    .WithMany(p => p.UsuarioPedidoDetalles)
                    .HasForeignKey(d => d.IdUsuarioPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuario_pedido_detalle_usuario_pedido");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasKey(e => e.IdVendedor);

                entity.ToTable("vendedor");

                entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("accion");

                entity.Property(e => e.FecUltModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_ult_modif");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.SnActivo).HasColumnName("sn_activo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
