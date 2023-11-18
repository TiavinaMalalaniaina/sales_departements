import MyRequestForm from "../forms/RequestProductForm"
import {
  CCard,
  CCardBody,
  CCardHeader,
  CCardTitle,
  CCol,
  CRow
} from '@coreui/react'
import RequestProductTable from "../tables/RequestProductTable"
import { useState } from "react"



const RequestProduct = () => {
    const [products, setProducts] = useState([]);
    const [formData, setFormData] = useState({
        key: '',
        product: '',
        quantity: '',
        reason: ''
    })
    const [action, setAction] = useState('Ajouter');
    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setFormData((prevFormData) => ({
          ...prevFormData,
          [name]: value
        }));
    };

    const updateProduct=(index)=> {
        setFormData((prevFormData) => ({
            ...prevFormData,
            product: products[index].product,
            quantity: products[index].quantity, 
            reason: products[index].reason,
            key: index
        }));
        setAction('Mettre à jour')
    }

    const addProduct=()=> {
        if (formData.key === '') {
            console.log(products)
            setProducts([...products, {product:formData.product, quantity:formData.quantity, reason:formData.reason}])
            setFormData((prevFormData) => ({
                ...prevFormData,
                key: '',
                product: '',
                quantity: '',
                reason: ''
            }))
        } else {
            const tempProducts = [...products]
            tempProducts[formData.key].product = formData.product
            tempProducts[formData.key].quantity = formData.quantity
            tempProducts[formData.key].motif = formData.motif
            setFormData((prevFormData) => ({
                ...prevFormData,
                key: '',
                product: '',
                quantity: '',
                motif: ''
            }))
            setAction('Ajouter')
        }
    }
    const removeProduct=(index)=> {
        const tempProducts = [...products]
        tempProducts.splice(index, 1)
        setProducts(tempProducts)
    }

    return (
        <>
            <CRow>
                <CCol xs={4}>
                    <CCard className="mb-4">
                        <CCardBody>
                            <CCardTitle><h4>Ajout de produit</h4></CCardTitle>
                            <MyRequestForm addProduct={addProduct} updateProduct={updateProduct} removeProduct={removeProduct} handleInputChange={handleInputChange} formData={formData}/>
                        </CCardBody>
                    </CCard>
                </CCol>
                <CCol xs={8}>
                    <CCard className="mb-4">
                        <CCardBody>
                            <CCardTitle><h4>Listes des produits à demander</h4></CCardTitle>
                            <RequestProductTable data={products} updateProduct={updateProduct} removeProduct={removeProduct}/>
                        </CCardBody>
                    </CCard>
                </CCol>
            </CRow>
        </>
    )
}
export default RequestProduct
