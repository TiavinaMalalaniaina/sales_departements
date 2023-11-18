import React from 'react'
import {
  CButton,
  CForm,
  CFormInput,
  CFormLabel,
  CFormSelect,
  CFormTextarea
} from '@coreui/react'

const RequestProductForm = ({formData, addProduct, handleInputChange}) => {

  const toDict=(data)=> {
    let model = []
    data.map(value=>{
      model[value.id]=value.name
    })
    return model;
  }

  const products = [
    {
      id: 1,
      name: "P1",
    },
    {
      id: 2,
      name: "P2",
    },
    {
      id: 3,
      name: "P3",
    },
    {
      id: 4,
      name: "P4",
    },
  ]

  const handleSubmit =(e)=> {
    e.preventDefault()
    const formData = new FormData(e.target)
    addProduct(formData)
  }

  return (
    <CForm onSubmit={handleSubmit}>
      <div className="mb-3">
        <CFormLabel htmlFor="request-form-product">Produit</CFormLabel>
        <CFormSelect 
        aria-label="Default select example" 
        name='product' 
        onChange={handleInputChange}
        value={formData.product}
        >
          <option>Choisissez votre produit</option>
          {products.map((product, index)=>
            <option value={product.id} key={index}>{product.name}</option>
          )}
        </CFormSelect>
      </div>
      <div className="mb-3">
        <CFormLabel htmlFor="request-form-quantity">Quantité</CFormLabel>
        <CFormInput type="number" name="quantity" id="request-form-quantity" placeholder="" onChange={handleInputChange}  value={formData.quantity}/>
      </div>
      <div className="mb-3">
        <CFormLabel htmlFor="request-form-reason">Motif</CFormLabel>
        <CFormTextarea type="text" name="reason" id="request-form-reason" placeholder="..." onChange={handleInputChange} value={formData.reason}/>
      </div>
      <CButton color="primary" type="submit">Ajouter</CButton>
    </CForm>
  )
}

export default RequestProductForm
