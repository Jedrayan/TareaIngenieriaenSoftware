/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     5/7/2020 19:11:14                            */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FORMULARIO_DONACION') and o.name = 'FK_FORMULAR_REFERENCE_USUARIO')
alter table FORMULARIO_DONACION
   drop constraint FK_FORMULAR_REFERENCE_USUARIO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FORMULARIO_DONACION') and o.name = 'FK_FORMULAR_REFERENCE_MARCAS')
alter table FORMULARIO_DONACION
   drop constraint FK_FORMULAR_REFERENCE_MARCAS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USUARIO') and o.name = 'FK_USUARIO_REFERENCE_ROL')
alter table USUARIO
   drop constraint FK_USUARIO_REFERENCE_ROL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FORMULARIO_DONACION')
            and   type = 'U')
   drop table FORMULARIO_DONACION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MARCAS')
            and   type = 'U')
   drop table MARCAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ROL')
            and   type = 'U')
   drop table ROL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIO')
            and   type = 'U')
   drop table USUARIO
go

/*==============================================================*/
/* Table: FORMULARIO_DONACION                                   */
/*==============================================================*/
create table FORMULARIO_DONACION (
   ID_FORMULARIO        numeric              identity,
   ID_USUARIO           int                  not null,
   ID_MARCA             int                  null,
   CI_DONANTE           varchar(15)          not null,
   NOMBRE_DONANTE       varchar(45)          not null,
   APELLIDO_DONANTE     varchar(45)          not null,
   LOTE_PRODUCTO_DONANCION varchar(45)          not null,
   FECHA_VENCIMIENTO_PRODUCTO datetime             not null,
   FECHA_ELABORACION_PRODUCTO datetime             not null,
   FECHA_ACOPIO_DONACION datetime             not null,
   FOTO                 image                not null,
   constraint PK_FORMULARIO_DONACION primary key (ID_FORMULARIO)
)
go

/*==============================================================*/
/* Table: MARCAS                                                */
/*==============================================================*/
create table MARCAS (
   ID_MARCA             int                  identity,
   NOMBRE_MARCA         varchar(25)          not null,
   constraint PK_MARCAS primary key (ID_MARCA)
)
go

/*==============================================================*/
/* Table: ROL                                                   */
/*==============================================================*/
create table ROL (
   ID_ROL               int                  identity,
   NOMBRE_ROL           varchar(25)          not null,
   constraint PK_ROL primary key (ID_ROL)
)
go

/*==============================================================*/
/* Table: USUARIO                                               */
/*==============================================================*/
create table USUARIO (
   ID_USUARIO           int                  identity,
   ID_ROL               int                  null,
   NOMBRE_USUARIO       varchar(45)          not null,
   APELLIDO_USUARIO     varchar(45)          not null,
   E_MAIL_USUARIO       varchar(45)          not null,
   PASSWORD_USUARIO     varchar(45)          not null,
   FECHA_CREACION_USUARIO timestamp            null,
   constraint PK_USUARIO primary key (ID_USUARIO)
)
go

alter table FORMULARIO_DONACION
   add constraint FK_FORMULAR_REFERENCE_USUARIO foreign key (ID_USUARIO)
      references USUARIO (ID_USUARIO)
go

alter table FORMULARIO_DONACION
   add constraint FK_FORMULAR_REFERENCE_MARCAS foreign key (ID_MARCA)
      references MARCAS (ID_MARCA)
go

alter table USUARIO
   add constraint FK_USUARIO_REFERENCE_ROL foreign key (ID_ROL)
      references ROL (ID_ROL)
go

