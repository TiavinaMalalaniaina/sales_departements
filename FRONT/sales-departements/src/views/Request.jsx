import { useState } from "react"
import RequestForm from "../components/forms/RequestForm"
import RequestTable from "../components/tables/RequestTable"

export default function Request() {
    const [products, setProducts] = useState([])
    const [formData, setFormData] = useState({
        key: '',
        product: '',
        quantity: '',
        motif: ''
    })
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
            motif: products[index].motif,
            key: index
        }));
    }

    const addProduct=()=> {
        if (formData.key === '') {
            setProducts([...products, {product:formData.product, quantity:formData.quantity, motif:formData.motif}])
            setFormData((prevFormData) => ({
                ...prevFormData,
                key: '',
                product: '',
                quantity: '',
                motif: ''
            }))
        } else {
            const tempProducts = [...products]
            tempProducts[formData.key].product = formData.product
            tempProducts[formData.key].quantity = formData.quantity
            tempProducts[formData.key].motif = formData.motif
            setFormData((prevFormData) => ({
                ...prevFormData,
                key: ''
            }))
            setFormData(tempProducts);
        }
    }
    const removeProduct=(index)=> {
        const tempProducts = [...products]
        tempProducts.splice(index, 1)
        setProducts(tempProducts)
    }
    return (
        <>
            <div class="col-lg-4 col-md-4">
                <div class="card">
                    <div class="header">
                        <h4 class="title">Demande de produit</h4>
                    </div>
                    <div class="content">
                        <RequestForm formData={formData} addProduct={addProduct} handleInputChange={handleInputChange}/>
                    </div>
                </div>
            </div>
            <div class="col-lg-8 col-md-4">
                <div class="card">
                    <div class="header">
                        <h4 class="title">Listes des produits</h4>
                    </div>
                    <div class="content">
                        <RequestTable data={products} removeProduct={removeProduct} updateProduct={updateProduct}/>
                    </div>
                </div>
            </div>
        </>
    )
}