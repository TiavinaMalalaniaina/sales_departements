import {
    CButton,
    CTable, 
    CTableBody, 
    CTableDataCell, 
    CTableHead, 
    CTableHeaderCell, 
    CTableRow
} from '@coreui/react'

import CIcon from '@coreui/icons-react';
import { cilPencil, cilX } from '@coreui/icons';

const RequestProductTable = ({data, updateProduct, removeProduct}) => {

    const toDict=(data)=> {
    let model = []
      data.map(value=>{
        model[value.id]=value.name
      })
      return model;
    }
    
    const all_products = [
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
    const products = toDict(all_products)

    const handleUpdate=(index)=> {
        updateProduct(index)
    }
    const handleRemove=(index)=> {
        removeProduct(index)
    }
    
    return (
        <CTable striped>
            <CTableHead>
                <CTableRow>
                    <CTableHeaderCell scope="col">#</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Produit</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Quantité</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Motif</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Action</CTableHeaderCell>
                </CTableRow>
            </CTableHead>
            <CTableBody>
                {data.map((value, index) => 
                <CTableRow key={index}>
                    <CTableHeaderCell scope="row">{index+1}</CTableHeaderCell>
                    <CTableDataCell>{products[value.product]}</CTableDataCell>
                    <CTableDataCell>{value.quantity}</CTableDataCell>
                    <CTableDataCell>{value.reason}</CTableDataCell>
                    <CTableDataCell>
                        {/* <CIcon icon={cilPencil} size='lg' onClick={handleUpdate(index)}/>
                        <CIcon icon={cilX} size='lg' onClick={handleRemove(index)}/> */}
                        <button></button>
                        <CIcon icon={cilPencil} size='lg'/>
                        <CIcon icon={cilX} size='lg'/>
                    </CTableDataCell>
                </CTableRow>
                )}
            </CTableBody>
        </CTable>
    )
}
export default RequestProductTable